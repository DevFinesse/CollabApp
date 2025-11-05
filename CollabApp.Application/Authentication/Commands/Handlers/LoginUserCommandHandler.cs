using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Contracts.Authentication;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Authentication;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Handlers
{
    public class LoginUserCommandHandler(IAuthService authService) : IRequestHandler<LoginUserCommand, Result<AuthResponse>>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result<AuthResponse>> Handle (LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GetTokenAsync(request.LoginRequest, cancellationToken);
        }
    }
}
