using CollabApp.Application.Users.Queries.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Users.Queries.Handlers
{
    public class GetProfileInfoQueryHandler(IUserService userService) : IRequestHandler<GetProfileInfoQuery, Result<UserProfileResponse>>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result<UserProfileResponse>> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetProfileInfoAsync(request.UserId);
        }
    }
}
