using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.CheckInAttendee;
public class CheckInAttendeeUseCase
{
    private readonly PassInDbContext _dbContext;
    public CheckInAttendeeUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        ValidateAttendee(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private void ValidateAttendee(Guid attendeeId)
    {
        var attendeeExists = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);

        if (!attendeeExists)
        {
            throw new NotFoundException("Attendee does not exists.");
        }

        var checkInExists = _dbContext.CheckIns.Any(checkIn => checkIn.Attendee_Id == attendeeId);

        if (checkInExists)
        {
            throw new ConflictException("Attendee cannot check in twice for the same event.");
        }
    }
}
