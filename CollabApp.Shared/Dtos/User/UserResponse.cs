namespace CollabApp.Shared.Dtos.User
{
    public record UserResponse
        (
           string Id,
           string FirstName,
           string LastName,
           string Email,
           string Status,
           bool IsDisabled,
           IEnumerable<string> Roles
        );
}
