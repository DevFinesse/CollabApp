using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Authentication
{
    public record RefreshTokenRequest(
    string Token,
    string RefreshToken
    );
}
