using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Contracts.Authentication;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Handlers
{
    public class RevokeRefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<RevokeRefreshTokenCommand, Result>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RevokeRefreshTokenAsync(request.RefreshTokenRequest, cancellationToken);
        }
    }
}
