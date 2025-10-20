using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CollabApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            // Let Identity handle Id and SecurityStamp generation
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Status { get; set; } = "offline"; // online, offline, away

        public bool IsDisabled { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<RoomMember> Members { get; set; } = [];
        public ICollection<ChatMember> Chats { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
