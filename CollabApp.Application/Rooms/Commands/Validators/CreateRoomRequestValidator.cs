using CollabApp.Shared.Dtos.Room;
using FluentValidation;

namespace CollabApp.Application.Rooms.Commands.Validators
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .Length(5, 50)
                .WithMessage("Name must be between 5 and 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(1000)
                .WithMessage("Description cannot be longer than 1000 characters");
        }
    }
}
