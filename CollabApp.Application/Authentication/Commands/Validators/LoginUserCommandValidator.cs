using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Shared.Dtos.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Authentication.Commands.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginRequest)
                    .NotNull()
                    .WithMessage("Login request cannot be null.")
                    .SetValidator(new LoginRequestValidator());
        }
    }
}
