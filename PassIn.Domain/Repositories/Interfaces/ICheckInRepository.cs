using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;
public interface ICheckInRepository
{
    Task<CheckIn> CheckInAttendee(Guid attendeeId);
    Task<CheckIn> FindByAttendeeId(Guid attendeeId);
}
