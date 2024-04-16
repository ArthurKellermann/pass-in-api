using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    private readonly IEventRepository eventRepository;
    public RegisterEventUseCase(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }
    public async Task<ResponseRegisteredJson> Execute(RequestEventJson request)
    {
        Validate(request);

        var entity = new Domain.Entities.Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-")
        };

        var registeredEvent = await this.eventRepository.Register(entity);

        return new ResponseRegisteredJson
        {
            Id = registeredEvent.Id,
        };
    }

    private void Validate(RequestEventJson request)
    {
        string errorMessage = string.Empty;

        if (request.MaximumAttendees <= 0)
        {
            errorMessage = "The maximum attendees is invalid.";
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            errorMessage = "The title is invalid.";
        }

        if (string.IsNullOrWhiteSpace(request.Details))
        {
            errorMessage = "The details are invalid.";
        }

        if (!string.IsNullOrEmpty(errorMessage)) {
            throw new ErrorOnValidationException(errorMessage);
        }      
    }
}
