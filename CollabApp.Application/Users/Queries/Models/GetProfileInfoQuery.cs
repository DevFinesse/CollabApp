using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Users.Queries.Models
{
    public record GetProfileInfoQuery(string UserId) : IRequest<Result<UserProfileResponse>>;

}
