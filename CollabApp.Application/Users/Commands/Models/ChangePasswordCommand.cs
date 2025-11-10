using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Commands.Models
{
    public record ChangePasswordCommand(string UserId, ChangePasswordRequest ChangePasswordRequest) : IRequest<Result>;
}
