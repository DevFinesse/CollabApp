using CollabApp.Application.Role.Commands.Models;
using CollabApp.Application.Role.Validator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Commands.Validators
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleRequest)
                .NotNull()
                .WithMessage("Role request cannot be null.")
                .SetValidator(new RoleRequestValidator());
        }
    }
}
