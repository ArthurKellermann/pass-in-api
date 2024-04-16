using FluentValidation;
using PassIn.Domain.Entities;

namespace PassIn.Application.Validators;

public class AttendeeValidator : AbstractValidator<Attendee>
{
    public AttendeeValidator()
    {
        RuleFor(attendee => attendee.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(attendee => attendee.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(attendee => attendee.EventId)
            .NotEmpty().WithMessage("EventId is required.");
    }
}

