using CollabApp.Application.Users.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Users.Commands.Handlers
{
    public class UnlockUserCommandHandler(IUserService userService) : IRequestHandler<UnlockUserCommand, Result>
    {
        private readonly IUserService _userService = userService;

        public Task<Result> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.UnlockAsync(request.UserId);
        }
    }

}
