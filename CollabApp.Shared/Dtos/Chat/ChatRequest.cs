using CollabApp.Shared.Dtos.ChatMember;
using CollabApp.Shared.Dtos.Message;
using CollabApp.Shared.Dtos.Room;

namespace CollabApp.Shared.Dtos.Chat
{
    public record ChatRequest
    {
        public Guid RoomId { get; init; }
        public string? Name { get; init; }
        public bool? IsGroup { get; init; }
        //public RoomRequest? Room { get; init; }
        public ICollection<MessageRequest>? Messages { get; init; }
        public ICollection<ChatMemberRequest>? Members { get; init; }
    }
}
