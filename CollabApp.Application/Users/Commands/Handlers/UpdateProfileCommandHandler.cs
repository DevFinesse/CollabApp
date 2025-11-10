using CollabApp.Application.Users.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Users.Commands.Handlers
{
    public class UpdateProfileCommandHandler(IUserService userService) : IRequestHandler<UpdateProfileCommand, Result>
    {
        private readonly IUserService _userService = userService;
        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateProfileAsync(request.UserId, request.UpdateProfileRequest);
        }
    }
}
