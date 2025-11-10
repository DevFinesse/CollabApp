using CollabApp.Application.Role.Commands.Models;
using CollabApp.Application.Users.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Users.Commands.Handlers
{
    public class ToggleUserStatusCommandHandler(IUserService userService) : IRequestHandler<ToggleUserStatusCommand, Result>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ToggleStatusAsync(request.UserId);
        }
    }
}
