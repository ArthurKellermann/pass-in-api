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
        ValidateAttendee(attendeeId);

        var checkIn = await this.checkInRepository.CheckInAttendee(attendeeId);

        return new ResponseRegisteredJson
        {
            Id = checkIn.Id,
        };
    }

    private async void ValidateAttendee(Guid attendeeId)
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
