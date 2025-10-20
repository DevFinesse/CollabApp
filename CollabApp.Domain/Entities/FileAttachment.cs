using System.ComponentModel.DataAnnotations.Schema;

namespace CollabApp.Domain.Entities
{
    public class FileAttachment
    {
        [Column("FileAttachmentId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Message))]
        public Guid MessageId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty ;
        public string FileUrl {  get; set; } = string.Empty ;   
        public long FIleSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow ;

        public Message? Message { get; set; }
    }
}
