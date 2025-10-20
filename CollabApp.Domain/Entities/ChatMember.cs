using System.ComponentModel.DataAnnotations.Schema;

namespace CollabApp.Domain.Entities
{
    public class ChatMember
    {
        [Column("ChatMemberId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId {  get; set; }

        public string Role { get; set; } = "Member";
        public DateTime? JoinedAt { get; set; } = DateTime.UtcNow;

        public Chat? Chat { get; set; }
        public User? User { get; set; }
    }
}
