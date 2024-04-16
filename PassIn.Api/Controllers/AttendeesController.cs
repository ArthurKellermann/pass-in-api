using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    [HttpPost]
    [Route("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult RegisterAttendeeOnEvent([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        var registerAttendeeOnEventUseCase = new RegisterAttendeeOnEventUseCase();

        var response = registerAttendeeOnEventUseCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAttendees([FromRoute] Guid eventId) 
    {
        return Ok();
    }
}
