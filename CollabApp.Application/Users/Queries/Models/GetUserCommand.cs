using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Queries.Models
{
    public record GetUserCommand(string UserId) : IRequest<Result<UserResponse>>;
}
