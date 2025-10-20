using System.ComponentModel.DataAnnotations.Schema;

namespace CollabApp.Domain.Entities
{
    public class Chat
    {
        [Column("ChatId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsGroup { get; set; }
        public Room? Room { get; set; }
        public ICollection<ChatMember> Members { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];

        
    }
}
