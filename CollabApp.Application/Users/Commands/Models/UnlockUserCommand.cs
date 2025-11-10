using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Users.Commands.Models
{
    public record UnlockUserCommand(string UserId) : IRequest<Result>;

}
