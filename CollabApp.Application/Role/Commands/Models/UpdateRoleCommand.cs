using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Commands.Models
{
    public record UpdateRoleCommand(string Id, RoleRequest RoleRequest) : IRequest<Result>;
}
