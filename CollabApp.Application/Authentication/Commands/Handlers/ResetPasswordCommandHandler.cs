using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Contracts.Authentication;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Handlers
{
    public class ResetPasswordCommandHandler(IAuthService authService) : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ResetPasswordAsync(request.ResetPasswordRequest);
        }
    }
}
