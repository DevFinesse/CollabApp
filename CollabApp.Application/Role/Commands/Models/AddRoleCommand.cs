using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Role;
using MediatR;

namespace CollabApp.Application.Role.Commands.Models
{
    public record AddRoleCommand(RoleRequest RoleRequest) : IRequest<Result<RoleResponseDetail>>;
}
