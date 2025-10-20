using CollabApp.Domain.Entities;
using CollabApp.Shared.Abstractions.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollabApp.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

        builder.OwnsMany(x => x.RefreshTokens)
        .ToTable("RefreshTokens")
        .WithOwner()
        .HasForeignKey("UserId");

        // Configure relationships
        builder.HasMany(u => u.Members)
            .WithOne(rm => rm.User)
            .HasForeignKey(rm => rm.UserId);

        builder.HasMany(u => u.Chats)
            .WithOne(cm => cm.User)
            .HasForeignKey(cm => cm.UserId);

        builder.HasMany(u => u.Messages)
            .WithOne(m => m.Sender)
            .HasForeignKey(m => m.SenderId);

        builder.HasData(new User
        {
            Id = DefaultUsers.Admin.Id,
            FirstName = "Restaurant System",
            LastName = "Admin",
            UserName = DefaultUsers.Admin.Email,
            NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
            Email = DefaultUsers.Admin.Email,
            NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
            SecurityStamp = DefaultUsers.Admin.SecurityStamp,
            ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
            EmailConfirmed = true,
            PasswordHash = DefaultUsers.Admin.PasswordHash
        });
    }
}