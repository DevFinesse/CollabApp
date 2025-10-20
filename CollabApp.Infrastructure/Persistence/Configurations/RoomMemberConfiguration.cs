using CollabApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollabApp.Infrastructure.Persistence.Configurations
{
    public class RoomMemberConfiguration : IEntityTypeConfiguration<RoomMember>
    {
        public void Configure(EntityTypeBuilder<RoomMember> builder)
        {
            builder.HasKey(rm => new { rm.RoomId, rm.UserId });

            builder.HasOne(rm => rm.Room)
                .WithMany(r => r.Members)
                .HasForeignKey(rm => rm.RoomId);

            builder.HasOne(rm => rm.User)
                .WithMany()
                .HasForeignKey(rm => rm.UserId);
        }
    }
}
