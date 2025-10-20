using CollabApp.Application.Users.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Commands.Handlers
{
    public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand, Result<UserResponse>>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateAsync(request.CreateUserRequest, cancellationToken);
        }
    }
}
