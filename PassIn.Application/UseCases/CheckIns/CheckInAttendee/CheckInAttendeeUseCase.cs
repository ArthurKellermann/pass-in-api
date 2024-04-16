using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.CheckIns.CheckInAttendee;
public class CheckInAttendeeUseCase
{
    private readonly ICheckInRepository checkInRepository;
    private readonly IAttendeeRepository attendeeRepository;
    public CheckInAttendeeUseCase(ICheckInRepository checkInRepository, IAttendeeRepository attendeeRepository)
    {
        this.checkInRepository = checkInRepository;
        this.attendeeRepository = attendeeRepository;
    }
    public async Task<ResponseRegisteredJson> Execute(Guid attendeeId)
    {
        await ValidateAttendee(attendeeId);

        var checkIn = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        var checkInRegistered = await this.checkInRepository.CheckInAttendee(attendeeId, checkIn);

        return new ResponseRegisteredJson
        {
            Id = checkInRegistered.Id,
        };
    }

    private async Task ValidateAttendee(Guid attendeeId)
    {
        Attendee attendeeExists = await this.attendeeRepository.FindById(attendeeId);

        if (attendeeExists is null)
        {
            throw new NotFoundException("Attendee does not exists.");
        }

        var checkInExists = await this.checkInRepository.FindByAttendeeId(attendeeId);

        if (checkInExists is not null)
        {
            throw new ConflictException("Attendee cannot check in twice for the same event.");
        }
    }
}
