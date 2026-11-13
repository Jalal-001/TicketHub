using FluentValidation;

namespace TicketHub.Application.Events.AddEvent
{
    public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
    {
        public AddEventCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Event name is required.")
                .MaximumLength(200).WithMessage("Event name must not exceed 200 characters.");
            RuleFor(x => x.Venue)
                .NotEmpty().WithMessage("Venue is required.")
                .MaximumLength(200).WithMessage("Venue must not exceed 200 characters.");
            RuleFor(x => x.StartsAt)
                .GreaterThan(_ => DateTime.UtcNow).WithMessage("Event start time must be in the future.");
            RuleFor(x => x.Rows)
                .InclusiveBetween(1, 20).WithMessage("Number of rows must be between 1 and 20.");
            RuleFor(x => x.SeatsPerRow)
                .InclusiveBetween(1, 50).WithMessage("Seats per row must be between 1 and 50.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be a positive value.");
        }
    }
}
