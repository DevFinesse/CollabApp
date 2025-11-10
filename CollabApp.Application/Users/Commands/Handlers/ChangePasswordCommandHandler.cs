using CollabApp.Application.Users.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Users.Commands.Handlers
{
    public class ChangePasswordCommandHandler(IUserService userService) : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result> Handle(ChangePasswordCommand request,  CancellationToken cancellationToken)
        {
            return await _userService.ChangePasswordAsync(request.UserId, request.ChangePasswordRequest);
        }
    }
}
