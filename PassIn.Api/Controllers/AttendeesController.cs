using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAllByEventId;
using PassIn.Application.UseCases.Attendees.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    private readonly RegisterAttendeeOnEventUseCase _registerAttendeeOnEventUseCase;
    private readonly GetAllAttendeesByEventIdUseCase _getAllAttendeesByEventIdUseCase;
    public AttendeesController(RegisterAttendeeOnEventUseCase registerAttendeeOnEventUseCase,
        GetAllAttendeesByEventIdUseCase getAllAttendeesByEventIdUseCase)
    {

        this._registerAttendeeOnEventUseCase = registerAttendeeOnEventUseCase;
        this._getAllAttendeesByEventIdUseCase = getAllAttendeesByEventIdUseCase;

    }

    [HttpPost]
    [Route("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterAttendeeOnEvent([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _registerAttendeeOnEventUseCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByEventId([FromRoute] Guid eventId)
    {
        var response = await _getAllAttendeesByEventIdUseCase.Execute(eventId);

        return Ok(response);
    }
}
