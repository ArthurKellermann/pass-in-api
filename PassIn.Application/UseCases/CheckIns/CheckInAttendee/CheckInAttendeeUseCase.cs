using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.CheckIns.CheckInAttendee;
public class CheckInAttendeeUseCase
{
    private readonly ICheckInRepository _checkInRepository;
    private readonly IAttendeeRepository _attendeeRepository;

    public CheckInAttendeeUseCase(ICheckInRepository checkInRepository, IAttendeeRepository attendeeRepository)
    {
        this._checkInRepository = checkInRepository;
        this._attendeeRepository = attendeeRepository;
    }

    public async Task<ResponseRegisteredJson> Execute(Guid attendeeId)
    {
        await ValidateAttendee(attendeeId);

        var checkIn = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        var checkInRegistered = await _checkInRepository.CheckInAttendee(attendeeId, checkIn);

        return new ResponseRegisteredJson
        {
            Id = checkInRegistered.Id,
        };
    }

    private async Task ValidateAttendee(Guid attendeeId)
    {
        Attendee attendeeExists = await _attendeeRepository.FindById(attendeeId);

        if (attendeeExists is null)
        {
            throw new NotFoundException("Attendee does not exists.");
        }

        var checkInExists = await _checkInRepository.FindByAttendeeId(attendeeId);

        if (checkInExists is not null)
        {
            throw new ConflictException("Attendee cannot check in twice for the same event.");
        }
    }
}
