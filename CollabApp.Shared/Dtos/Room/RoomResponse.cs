namespace CollabApp.Shared.Dtos.Room
{
    public record RoomResponse
    (
        Guid Id,
        string? Name,
        string? Description,
        string? CreatedBy,
        DateTime? CreatedAt
     );
}
