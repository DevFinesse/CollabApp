using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Commands.Models
{
    public record UpdateProfileCommand(string UserId, UpdateProfileRequest UpdateProfileRequest) : IRequest<Result>;

}
