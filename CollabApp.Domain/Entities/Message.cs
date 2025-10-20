using System.ComponentModel.DataAnnotations.Schema;

namespace CollabApp.Domain.Entities
{
    public class Message
    {
        [Column("MessageId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }

        [ForeignKey(nameof(User))]
        public string? SenderId { get; set; }

        public string Content { get; set; } = string.Empty;
        public string Type { get; set; } = "text"; //text, File, Gif
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Chat? Chat { get; set; }
        public User? Sender {  get; set; }
        public ICollection<FileAttachment> Attachments { get; set; } = [];

    }
}
