using Microsoft.AspNetCore.Mvc;

namespace TestProj_ABP_Backend.Controllers;

[Route("experiment/")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id"></param>
    /// <returns>HTTP responce.</returns>
    /// <response code="200">Succesfully founded</response>
    [HttpGet("button-color")]
    [ProducesResponseType(200)]
    public IActionResult ButtonColor([FromQuery] string id)
    {
        return Ok(new[] { "value1", "value2" });
    }

}

