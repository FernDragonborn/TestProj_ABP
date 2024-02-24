using Microsoft.AspNetCore.Mvc;
using TestProj_ABP_Backend.AB_Tests;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.Controllers;

[Route("experiment/")]
[ApiController]
public class UserController : ControllerBase
{
    IConfiguration _configuration;
    UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
        DeviceToken = DeviceToken.Trim();
        Result<string> strResult = ColorTest.GetColor(DeviceToken, _configuration);

        if (strResult.IsSuccess == false)
        {
            var user = UserService.RegisterUser(_configuration);
            DeviceToken = user.DeviceToken;

            //if user was regitered, so it's in DB and result would be positive in any outcome
            ColorTest.AssignColor(DeviceToken, _configuration);
            strResult = ColorTest.GetColor(DeviceToken, _configuration);
        }
        var jsonData = new { key = "button-color", value = strResult.Data };
        return new JsonResult(jsonData);
    }

    [HttpPost]
    public IActionResult GetUserFingerprint([FromBody] UserFingerprintDto? fingerprintDto)
    {


        if (fingerprintDto is not null) return Ok();
        else return BadRequest("Dto was null or undefined");
    }
}

