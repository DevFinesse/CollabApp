using System.ComponentModel.DataAnnotations.Schema;

namespace CollabApp.Domain.Entities
{
    public class Room
    {
        [Column("RoomId")]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [ForeignKey(nameof(User))]
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RoomMember> Members { get; set; } = [];
        public ICollection<Chat> Chats { get; set; } = [];
    }
}
