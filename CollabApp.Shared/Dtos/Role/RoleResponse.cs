using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Role
{
    public record RoleResponse(
    string Id,
    string Name,
    bool IsDeleted
    );
}
