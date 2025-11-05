using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Authentication;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Models
{
    public record RevokeRefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : IRequest<Result>;
}
