using CollabApp.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Authentication.Commands.Models
{
    public record SendResetPasswordCodeCommand(string Email) : IRequest<Result>;
    
}
