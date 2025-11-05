using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Authentication;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Models
{
    public record LoginUserCommand(LoginRequest LoginRequest) : IRequest<Result<AuthResponse>>;

}
