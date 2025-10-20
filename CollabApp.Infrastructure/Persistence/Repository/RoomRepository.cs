using CollabApp.Contracts.Repository;
using CollabApp.Domain.Entities;
using System.Linq.Expressions;

namespace CollabApp.Infrastructure.Persistence.Repository
{
    public class RoomRepository : IRoomRepository, IRepositoryBase<Room>
    {
        public Task AddAsync(Room entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Room> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Room entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRange(IEnumerable<Room> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> FindAllAsync(Expression<Func<Room, bool>> criteria, CancellationToken cancellationToken = default, params Expression<Func<Room, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Room> FindAsync(Expression<Func<Room, bool>> criteria, CancellationToken cancellationToken = default, params Expression<Func<Room, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Room> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Room entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
