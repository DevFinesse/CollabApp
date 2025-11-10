using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Role.Commands.Models
{
    public record ToggleRoleStatusCommand(string Id) : IRequest<Result>;
}
