using FluentValidation;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.Attendees.RegisterAttendee
{
    public class RegisterAttendeeOnEventUseCase
    {
        private readonly IAttendeeRepository attendeeRepository;
        private readonly IEventRepository eventRepository;
        private readonly AbstractValidator<Attendee> attendeeValidator;

        public RegisterAttendeeOnEventUseCase(
            IAttendeeRepository attendeeRepository,
            IEventRepository eventRepository,
            AbstractValidator<Attendee> attendeeValidator)
        {
            this.attendeeRepository = attendeeRepository;
            this.eventRepository = eventRepository;
            this.attendeeValidator = attendeeValidator;
        }

        public async Task<ResponseRegisteredJson> Execute(Guid eventId, RequestRegisterEventJson request)
        {
            await ValidateEvent(request, eventId);

            var attendee = new Attendee
            {
                Name = request.Name,
                Email = request.Email,
                EventId = eventId,
            };

            var validationResult = this.attendeeValidator.Validate(attendee);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage);
                throw new ErrorOnValidationException(string.Join(", ", errors));
            }

            var attendeeOnEvent = await attendeeRepository.RegisterOnEvent(eventId, attendee);

            return new ResponseRegisteredJson
            {
                Id = attendeeOnEvent.Id,
            };
        }

        private async Task ValidateEvent(RequestRegisterEventJson request, Guid eventId)
        {
            var eventEntity = await this.eventRepository.GetEventById(eventId);

            if (eventEntity is null)
            {
                throw new NotFoundException("Event does not exist.");
            }

            var attendeeAlreadyRegistered = await this.attendeeRepository.IsRegisteredForEvent(request.Email, eventId);

            if (attendeeAlreadyRegistered)
            {
                throw new ConflictException("You cannot register twice for the same event.");
            }

            var attendeesForEvent = await this.eventRepository.GetNumberOfAttendeesByEventId(eventId);

            if (attendeesForEvent == eventEntity.Maximum_Attendees)
            {
                throw new ErrorOnValidationException("There is no room for this event.");
            }
        }
    }
}
