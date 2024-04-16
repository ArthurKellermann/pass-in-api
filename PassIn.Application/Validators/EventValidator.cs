using FluentValidation;
using PassIn.Domain.Entities;

namespace PassIn.Application.Validators;
public class EventValidator : AbstractValidator<Event>
{
    public EventValidator()
    {
        RuleFor(ev => ev.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(ev => ev.Details)
            .NotEmpty().WithMessage("Details is required.")
            .MaximumLength(255).WithMessage("Details must not exceed 255 characters.");

        RuleFor(ev => ev.Maximum_Attendees)
            .NotEmpty().WithMessage("Maximum Attendees is required.")
            .GreaterThan(0).WithMessage("Maximum Attendees must be greater than 0.")
            .LessThanOrEqualTo(255).WithMessage("Maximum Attendees must not exceed 255.");
    }
}