using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Authentication;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Models
{
    public record GenerateRefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : IRequest<Result<AuthResponse>>;

}
