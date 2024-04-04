using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Database;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public ResponseRegisteredJson Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var entity = new Domain.Entities.Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-")
        };

        dbContext.Events.Add(entity);
        dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
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
