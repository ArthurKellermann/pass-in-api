using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId;
public class GetAllAttendeesByEventIdUseCase
{
    private readonly IAttendeeRepository attendeeRepository;
    public GetAllAttendeesByEventIdUseCase(IAttendeeRepository attendeeRepository)
    {
        this.attendeeRepository = attendeeRepository;
    }
    public async Task<ResponseAllAttendeesJson> Execute(Guid eventId)
    {
        List<Attendee> attendees = await this.attendeeRepository.GetAllByEventId(eventId);

        if (attendees is null)
        {
            throw new NotFoundException("Event does not exists.");
        }

        return new ResponseAllAttendeesJson
        {
            Attendees = attendees.Select((attendee) => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at,
            }).ToList(),
        };
    }

}
