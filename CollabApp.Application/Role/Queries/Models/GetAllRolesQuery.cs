using CollabApp.Shared.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Queries.Models
{
    public record GetAllRolesQuery(bool IncludeDisabled = false) : IRequest<IEnumerable<RoleResponse>>;

}
