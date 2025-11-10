using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Users.Commands.Models
{
    public record ToggleUserStatusCommand(string UserId) : IRequest<Result>;
}
