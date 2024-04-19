using FluentValidation;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly AbstractValidator<Event> _eventvalidator;

    public RegisterEventUseCase(IEventRepository eventRepository, AbstractValidator<Event> eventvalidator)
    {
        this._eventRepository = eventRepository;
        this._eventvalidator = eventvalidator;
    }

    public async Task<ResponseRegisteredJson> Execute(RequestEventJson request)
    {
        var entity = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-")
        };
        var validationResult = _eventvalidator.Validate(entity);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            throw new ErrorOnValidationException(string.Join(", ", errors));
        }

        var registeredEvent = await _eventRepository.Register(entity);

        return new ResponseRegisteredJson
        {
            Id = registeredEvent.Id,
        };
    }
}
