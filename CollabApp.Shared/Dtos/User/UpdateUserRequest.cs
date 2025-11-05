namespace CollabApp.Shared.Dtos.User
{
    public record UpdateUserRequest
    (
        string FirstName,
        string LastName,
        string Email,
        ICollection<string> Roles
    );
}
