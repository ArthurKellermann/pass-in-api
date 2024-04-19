using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.CheckInAttendee;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    private readonly CheckInAttendeeUseCase _checkInAttendeetUseCase;
    public CheckInController(CheckInAttendeeUseCase checkInAttendeetUseCase)
    {

        this._checkInAttendeetUseCase = checkInAttendeetUseCase;

    }
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckIn([FromRoute] Guid attendeeId)
    {
        var response = await _checkInAttendeetUseCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}
