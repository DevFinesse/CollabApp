using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Queries.Models
{
    public record GetRoleQuery(string RoleId) : IRequest<Result<RoleResponseDetail>>;

}
