using CollabApp.Application.Settings;
using CollabApp.Domain.Entities;
using CollabApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CollabApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
             services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, ApplicationRole>(o => {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
            
            if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.Key))
            {
                throw new InvalidOperationException("JWT configuration is missing or invalid.");
            }

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
     .AddJwtBearer(options =>
   {
         options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
       ValidateAudience = true,
           ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
          ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
     };
     });
        }

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
      services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        }
    }
}
