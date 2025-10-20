using CollabApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollabApp.Infrastructure.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        // Configure relationships
        builder.HasMany(r => r.Members)
            .WithOne(rm => rm.Room)
            .HasForeignKey(rm => rm.RoomId);

        builder.HasMany(r => r.Chats)
            .WithOne(c => c.Room)
            .HasForeignKey(c => c.RoomId);
    }
}