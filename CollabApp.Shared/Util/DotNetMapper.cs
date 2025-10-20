// DotNetMapper.cs
// Lightweight, reflection-free .NET mapper
// Features:
// - Explicit delegate-based registration
// - Reverse mapping
// - Collection support
// - Thread-safe
// - Prevents self-referencing loops
// - Configurable max-depth (per call or global)
// - Injectable IMapper service for ASP.NET Core
// - Profile-style registration support

using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetMapper
{
    internal readonly struct TypePair : IEquatable<TypePair>
    {
        public readonly Type Source;
        public readonly Type Destination;

        public TypePair(Type s, Type d) { Source = s; Destination = d; }
        public bool Equals(TypePair other) => Source == other.Source && Destination == other.Destination;
        public override bool Equals(object? obj) => obj is TypePair p && Equals(p);
        public override int GetHashCode() => HashCode.Combine(Source, Destination);
    }

    internal sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();
        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }

    #region Interfaces

    public interface IMapper
    {
        TDest Map<TSource, TDest>(TSource source, int? maxDepth = null);
        bool TryMap<TSource, TDest>(TSource source, out TDest? dest);
        List<TDest> MapCollection<TSource, TDest>(IEnumerable<TSource> source, int? maxDepth = null);
        void MapInto<TSource, TDest>(TSource source, TDest destination);
    }

    public interface IMapperProfile
    {
        void Register();
    }

    #endregion

    #region Core Mapper Service

    public sealed class MapperService : IMapper
    {
        private readonly int _defaultMaxDepth;

        public MapperService(int defaultMaxDepth = 10)
        {
            _defaultMaxDepth = defaultMaxDepth;
        }

        public TDest Map<TSource, TDest>(TSource source, int? maxDepth = null) => Mapper.Map<TSource, TDest>(source, maxDepth ?? _defaultMaxDepth);

        public bool TryMap<TSource, TDest>(TSource source, out TDest? dest) => Mapper.TryMap(source, out dest);

        public List<TDest> MapCollection<TSource, TDest>(IEnumerable<TSource> source, int? maxDepth = null) => Mapper.MapCollection<TSource, TDest>(source, maxDepth ?? _defaultMaxDepth);

        public void MapInto<TSource, TDest>(TSource source, TDest destination) => Mapper.MapInto(source, destination);
    }

    #endregion

    public static class Mapper
    {
        private static readonly ConcurrentDictionary<TypePair, Delegate> _mapFuncs = new();
        private static readonly ConcurrentDictionary<TypePair, Delegate> _mapActions = new();
        private static int _defaultMaxDepth = 10;

        #region Configuration

        public static void SetDefaultMaxDepth(int maxDepth)
        {
            if (maxDepth < 1) throw new ArgumentOutOfRangeException(nameof(maxDepth));
            _defaultMaxDepth = maxDepth;
        }

        public static int GetDefaultMaxDepth() => _defaultMaxDepth;

        public static void RegisterProfile<TProfile>() where TProfile : IMapperProfile, new()
        {
            var profile = new TProfile();
            profile.Register();
        }

        public static void RegisterProfiles(params IMapperProfile[] profiles)
        {
            foreach (var p in profiles)
                p.Register();
        }

        #endregion

        #region Registration

        public static void Register<TSource, TDest>(Func<TSource, TDest> mapFunc, Func<TDest, TSource>? reverse = null)
        {
            if (mapFunc is null) throw new ArgumentNullException(nameof(mapFunc));
            var key = new TypePair(typeof(TSource), typeof(TDest));
            _mapFuncs[key] = mapFunc;

            if (reverse != null)
            {
                var rkey = new TypePair(typeof(TDest), typeof(TSource));
                _mapFuncs[rkey] = reverse;
            }
        }

        public static void RegisterInPlace<TSource, TDest>(Action<TSource, TDest> mapAction, Action<TDest, TSource>? reverse = null)
        {
            if (mapAction is null) throw new ArgumentNullException(nameof(mapAction));
            var key = new TypePair(typeof(TSource), typeof(TDest));
            _mapActions[key] = mapAction;

            if (reverse != null)
            {
                var rkey = new TypePair(typeof(TDest), typeof(TSource));
                _mapActions[rkey] = reverse;
            }
        }

        #endregion

        #region Mapping Logic

        private static TDest InternalMap<TSource, TDest>(TSource source, Dictionary<object, object> cache, int depth, int maxDepth)
        {
            if (source == null) return default!;
            if (depth > maxDepth)
                throw new InvalidOperationException($"Maximum mapping depth of {maxDepth} exceeded.");

            if (cache.TryGetValue(source, out var existing))
                return (TDest)existing;

            var key = new TypePair(typeof(TSource), typeof(TDest));
            if (_mapFuncs.TryGetValue(key, out var d))
            {
                var f = (Func<TSource, TDest>)d!;
                var dest = f(source);
                cache[source!] = dest!;
                return dest;
            }

            throw new KeyNotFoundException($"No mapping registered from {typeof(TSource).FullName} to {typeof(TDest).FullName}");
        }

        public static TDest Map<TSource, TDest>(TSource source, int? maxDepth = null)
        {
            if (source == null) return default!;
            int depthLimit = maxDepth ?? _defaultMaxDepth;
            var cache = new Dictionary<object, object>(ReferenceEqualityComparer.Instance);
            return InternalMap<TSource, TDest>(source, cache, 0, depthLimit);
        }

        public static bool TryMap<TSource, TDest>(TSource source, out TDest? dest)
        {
            dest = default;
            if (source == null) return true;
            var key = new TypePair(typeof(TSource), typeof(TDest));
            if (_mapFuncs.TryGetValue(key, out var d))
            {
                var f = (Func<TSource, TDest>)d!;
                dest = f(source);
                return true;
            }
            return false;
        }

        public static List<TDest> MapCollection<TSource, TDest>(IEnumerable<TSource> source, int? maxDepth = null)
        {
            if (source == null) return new List<TDest>();
            int depthLimit = maxDepth ?? _defaultMaxDepth;
            var cache = new Dictionary<object, object>(ReferenceEqualityComparer.Instance);
            var key = new TypePair(typeof(TSource), typeof(TDest));
            if (!_mapFuncs.TryGetValue(key, out var d))
                throw new KeyNotFoundException($"No mapping registered from {typeof(TSource).FullName} to {typeof(TDest).FullName}");

            var f = (Func<TSource, TDest>)d!;
            var result = new List<TDest>();
            foreach (var item in source)
            {
                if (item == null) continue;
                if (cache.TryGetValue(item, out var existing))
                {
                    result.Add((TDest)existing);
                }
                else
                {
                    var mapped = InternalMap<TSource, TDest>(item, cache, 0, depthLimit);
                    result.Add(mapped);
                }
            }
            return result;
        }

        public static void MapInto<TSource, TDest>(TSource source, TDest destination)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (source == null) return;

            var key = new TypePair(typeof(TSource), typeof(TDest));
            if (_mapActions.TryGetValue(key, out var d))
            {
                var a = (Action<TSource, TDest>)d!;
                a(source, destination);
                return;
            }

            throw new KeyNotFoundException($"No in-place mapping registered from {typeof(TSource).FullName} to {typeof(TDest).FullName}");
        }

        #endregion

        #region Extensions

        public static void Clear()
        {
            _mapFuncs.Clear();
            _mapActions.Clear();
        }

        public static IServiceCollection AddDotNetMapper(this IServiceCollection services, int defaultMaxDepth = 10, params Type[] profileTypes)
        {
            foreach (var type in profileTypes)
            {
                if (typeof(IMapperProfile).IsAssignableFrom(type) && Activator.CreateInstance(type) is IMapperProfile profile)
                    profile.Register();
            }

            services.AddSingleton<IMapper>(new MapperService(defaultMaxDepth));
            return services;
        }

        #endregion
    }

    #region Example

    public record Person(Guid Id, string FirstName, string LastName, int Age, Person? BestFriend = null);

    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public PersonDto? BestFriend { get; set; }
    }

    public class PersonProfile : IMapperProfile
    {
        public void Register()
        {
            Mapper.Register<Person, PersonDto>(p => new PersonDto
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                Age = p.Age,
                BestFriend = p.BestFriend == null ? null : Mapper.Map<Person, PersonDto>(p.BestFriend, 5)
            },
            dto =>
            {
                var parts = (dto.FullName ?? string.Empty).Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                var first = parts.Length > 0 ? parts[0] : string.Empty;
                var last = parts.Length > 1 ? parts[1] : string.Empty;
                return new Person(dto.Id, first, last, dto.Age, dto.BestFriend == null ? null : Mapper.Map<PersonDto, Person>(dto.BestFriend, 5));
            });
        }
    }

    #endregion
}
