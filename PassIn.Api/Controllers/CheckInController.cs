using Microsoft.AspNetCore.Mvc;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult CheckIn([FromRoute] Guid attendeeId)
    {

    }
}
