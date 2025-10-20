using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.User
{
    public record CreateUserRequest
        (
            string FirstName,
            string LastName,
            string UserName,
            string Email,
            string Password,
            List<string> Roles
        );
    
}
