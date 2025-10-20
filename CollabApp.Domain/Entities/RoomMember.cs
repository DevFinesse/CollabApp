using System.ComponentModel.DataAnnotations.Schema;

namespace CollabApp.Domain.Entities
{
    public class RoomMember
    {
        [Column("RoomMemberId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }

        [ForeignKey(nameof (User))]
        public string? UserId {  get; set; }

        public string Role { get; set; } = "Member"; //owner, member, admin
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Room? Room { get; set; }
        public User? User { get; set; }
    }
}
