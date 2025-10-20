using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Role
{
    public record RoleResponseDetail(
    string Id,
    string Name,
    bool IsDeleted,
    IEnumerable<string> Permissions
);
}
