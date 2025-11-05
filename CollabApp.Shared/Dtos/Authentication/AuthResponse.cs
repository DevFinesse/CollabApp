using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Authentication
{
    public record AuthResponse(
    string Id,
    string? Email,
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);
}
