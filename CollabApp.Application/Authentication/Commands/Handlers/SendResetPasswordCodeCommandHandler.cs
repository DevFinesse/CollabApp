using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Contracts.Authentication;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Authentication.Commands.Handlers
{
    public class SendResetPasswordCodeCommandHandler(IAuthService authService) : IRequestHandler<SendResetPasswordCodeCommand, Result>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Result> Handle(SendResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            return await _authService.SendResetPasswordCodeAsync(request.Email);
        }
    }
}
