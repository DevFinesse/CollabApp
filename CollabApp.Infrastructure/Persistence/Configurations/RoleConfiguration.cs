using CollabApp.Domain.Entities;
using CollabApp.Shared.Abstractions.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CollabApp.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            // Default Data
            builder.HasData([
                new ApplicationRole{
                Id = DefaultRoles.Admin.Id,
                Name = DefaultRoles.Admin.Name,
                NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp,
            },
            new ApplicationRole{
                Id = DefaultRoles.Moderator.Id,
                Name = DefaultRoles.Moderator.Name,
                NormalizedName = DefaultRoles.Moderator.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Moderator.ConcurrencyStamp,
            },
                new ApplicationRole{
                Id = DefaultRoles.Creator.Id,
                Name = DefaultRoles.Creator.Name,
                NormalizedName = DefaultRoles.Creator.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Creator.ConcurrencyStamp,
            },
                new ApplicationRole{
                Id = DefaultRoles.Member.Id,
                Name = DefaultRoles.Member.Name,
                NormalizedName = DefaultRoles.Member.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Member.ConcurrencyStamp,
            }

        ]);
        }
    }
}
