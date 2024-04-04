using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.CheckInAttendee;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult CheckIn([FromRoute] Guid attendeeId)
    {
        var checkInAttendeeUseCase = new CheckInAttendeeUseCase();

        var response = checkInAttendeeUseCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}
