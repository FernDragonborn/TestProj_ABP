using Microsoft.AspNetCore.Mvc;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.Controllers;

[Route("experiment/")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    /// Get
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <returns>HTTP responce.</returns>
    /// <response code="200">Succesfully founded</response>
    [ProducesResponseType(200)]
    [HttpGet("button-color")]
    public IActionResult ButtonColor([FromQuery(Name = "device-token")] string? DeviceToken)
    {
        if (!string.IsNullOrWhiteSpace(DeviceToken))
        {
            return Ok(new[] { "value1", "value2" });
        }
        DeviceToken.Trim();
        return Ok(UserService.GenerateRandomString(6));
    }
}

