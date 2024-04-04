using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;
public interface IAttendeeRepository
{
    Task<List<Attendee>> GetAllByEventId(Guid eventId);
    Task<Attendee> FindById(Guid id);
}
