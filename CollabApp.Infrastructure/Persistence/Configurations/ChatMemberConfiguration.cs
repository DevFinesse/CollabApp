using CollabApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabApp.Infrastructure.Persistence.Configurations
{
    public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
    {
        public void Configure(EntityTypeBuilder<ChatMember> builder)
        {
            builder.HasKey(cm => new { cm.ChatId, cm.UserId });

            builder.HasOne(cm => cm.Chat)
                .WithMany(c => c.Members)
                .HasForeignKey(cm => cm.ChatId);

            builder.HasOne(cm => cm.User)
                .WithMany()
                .HasForeignKey(cm => cm.UserId);
        }
    }
}
