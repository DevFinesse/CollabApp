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
    public class FileAttachmentConfiguration : IEntityTypeConfiguration<FileAttachment>
    {
        public void Configure(EntityTypeBuilder<FileAttachment> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(f => f.FileType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.FileUrl)
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasOne(f => f.Message)
                .WithMany(m => m.Attachments)
                .HasForeignKey(f => f.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
