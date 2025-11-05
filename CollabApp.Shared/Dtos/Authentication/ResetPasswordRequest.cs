using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Authentication
{
    public record ResetPasswordRequest(
    string Email,
    string Code,
    string NewPassword
);
}
