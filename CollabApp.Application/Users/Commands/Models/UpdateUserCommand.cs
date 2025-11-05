using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Commands.Models
{
    public record UpdateUserCommand (string UserId, UpdateUserRequest UpdateUserRequest) : IRequest<Result>;
}
