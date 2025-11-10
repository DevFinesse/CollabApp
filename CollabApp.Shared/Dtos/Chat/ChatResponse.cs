namespace CollabApp.Shared.Dtos.Chat
{
    public record ChatResponse
    (
        Guid Id,
        Guid RoomId,
        string Name
        );
}
