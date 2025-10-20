using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Commands.Models
{
    public record CreateUserCommand(CreateUserRequest CreateUserRequest) : IRequest<Result<UserResponse>>;
}
