using CollabApp.Application.Users.Queries.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;

namespace CollabApp.Application.Users.Queries.Handlers
{
    public class GetUserCommandHandler(IUserService userService) : IRequestHandler<GetUserCommand, Result<UserResponse>>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result<UserResponse>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.GetAsync(request.UserId);
        }
    }
}
