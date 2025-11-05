using CollabApp.Application.Users.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Users.Commands.Handlers
{
    public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateAsync(request.UserId, request.UpdateUserRequest, cancellationToken);
        }
    }
}
