using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Attendees.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly IAttendeeRepository attendeeRepository;
    private readonly IEventRepository eventRepository;
    public RegisterAttendeeOnEventUseCase(IAttendeeRepository attendeeRepository, IEventRepository eventRepository)
    {
        this.attendeeRepository = attendeeRepository;
        this.eventRepository = eventRepository;
    }
    public async Task<ResponseRegisteredJson> Execute(Guid eventId, RequestRegisterEventJson request)
    {
        ValidateEvent(request, eventId);

        var attendee = new Attendee
        {
            Name = request.Name,
            Email = request.Email,
            EventId = eventId,
        };

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
            throw new NotFoundException("Event does not exists.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("The name is invalid.");
        }

        if (!ValidateEmail(request.Email))
        {
            throw new ErrorOnValidationException("The email is invalid.");
        }

        var attendeeAlreadyRegistered = _dbContext
            .Attendees
            .Any(attendee => attendee.Email.Equals(request.Email) &&
            attendee.EventId.Equals(eventId));

        if (attendeeAlreadyRegistered)
        {
            throw new ConflictException("You cannot register twice for the same event.");
        }

        var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.EventId == eventId);

        if (attendeesForEvent == eventEntity.Maximum_Attendees)
        {
            throw new ErrorOnValidationException("There is no room for this event.");
        }
    }

    private bool ValidateEmail(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }

    }
}
