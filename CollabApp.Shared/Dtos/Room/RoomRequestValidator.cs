using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Room
{
    public class RoomRequestValidator : AbstractValidator<RoomRequest>
    {
        public RoomRequestValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name Length cannot be more than 50");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(1000)
                .WithMessage("Description Length cannot be longer than 1000");

        }
    }
}
