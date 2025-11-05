using CollabApp.Application.Authentication.Commands.Models;
using FluentValidation;

namespace CollabApp.Application.Authentication.Commands.Validators
{
    public class SendResetPasswordCodeCommandValidator : AbstractValidator<SendResetPasswordCodeCommand>
    {
        public SendResetPasswordCodeCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("A valid email is required");
        }
    }
}
