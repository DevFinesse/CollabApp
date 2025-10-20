using CollabApp.Contracts.Authentication;
using CollabApp.Contracts.Repository;
using CollabApp.Contracts.Services;
using CollabApp.Extensions;
using CollabApp.Infrastructure.Persistence.Repository;
using CollabApp.Infrastructure.Services;
using CollabApp.Infrastructure.Authentication;

namespace CollabApp.Extensions
{
    public static class ServiceRegistrationExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.ConfigureCors();
            services.ConfigureSqlContext(configuration);
            services.ConfigureIdentity();
            services.AddJwtConfiguration(configuration);
            services.ConfigureJWT(configuration);
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

        }
    }
}
