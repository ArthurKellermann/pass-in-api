using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
    {
        ValidateEvent(request, eventId);

        var entity = new Infrastructure.Entities.Attendee
        {
            Name = request.Name,
            Email = request.Email,
            EventId = eventId,
            Created_At = DateTime.UtcNow,
        };

        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private void ValidateEvent(RequestRegisterEventJson request, Guid eventId)
    {
        var eventEntity = _dbContext.Events.Find(ev => ev.Id == eventId);

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
