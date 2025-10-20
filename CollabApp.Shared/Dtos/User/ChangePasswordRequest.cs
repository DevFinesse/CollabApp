namespace CollabApp.Shared.Dtos.User
{
    public record ChangePasswordRequest
    (
        string CurrentPassword,
        string NewPassword
        );
}
