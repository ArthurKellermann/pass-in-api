using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;
public interface IAttendeeRepository
{
    Task<Attendee> RegisterOnEvent(Guid eventId,  Attendee attendee);
    Task<List<Attendee>> GetAllByEventId(Guid eventId);
    Task<Attendee> FindById(Guid id);
}
