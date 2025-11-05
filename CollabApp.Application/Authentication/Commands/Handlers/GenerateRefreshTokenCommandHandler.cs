using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Contracts.Authentication;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Authentication.Commands.Handlers
{
    public class GenerateRefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<GenerateRefreshTokenCommand, Result<AuthResponse>>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result<AuthResponse>> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.GetRefreshTokenAsync(request.RefreshTokenRequest, cancellationToken);
        }
    }
}
