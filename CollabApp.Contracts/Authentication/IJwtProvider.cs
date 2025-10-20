using CollabApp.Domain.Entities;

namespace CollabApp.Contracts.Authentication
{
    public interface IJwtProvider
    {
        (string token, int expiresIn) GenerateJwtToken(User user, IEnumerable<string> roles, IEnumerable<string> permissions);
        string? ValidateToken(string token);
    }
}
