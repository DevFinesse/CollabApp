using CollabApp.Application.Authentication.Commands.Models;
using CollabApp.Shared.Dtos.Authentication;
using FluentValidation;

namespace CollabApp.Application.Authentication.Commands.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.ResetPasswordRequest)
                .NotNull()
                .WithMessage("Reset password request is required.")
                .SetValidator(new ResetPasswordRequestValidator());

        }
    }
}
