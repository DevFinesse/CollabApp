using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Shared.Dtos.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Authentication.Commands.Validators
{
    public class GenerateRefreshTokenCommandValidator : AbstractValidator<GenerateRefreshTokenCommand>
    {
        public GenerateRefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshTokenRequest)
                .NotNull()
                .WithMessage("Refresh token request cannot be null.")
                .SetValidator(new RefreshTokenRequestValidator());
        }
    }
}
