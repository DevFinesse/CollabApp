using CollabApp.Application.Role.Commands.Models;
using CollabApp.Application.Role.Validator;
using FluentValidation;

namespace CollabApp.Application.Role.Commands.Validators
{
    public class AddRoleCommandValidator :  AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator() 
        {
            RuleFor(x => x.RoleRequest)
            .NotNull()
            .WithMessage("Role request cannot be null.")
            .SetValidator(new RoleRequestValidator());
        }
    }
}
