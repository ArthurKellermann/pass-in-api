using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;
public interface ICheckInRepository
{
    Task<CheckIn> CheckInAttendee(Guid attendeeId, CheckIn checkIn);
    Task<CheckIn> FindByAttendeeId(Guid attendeeId);
}
