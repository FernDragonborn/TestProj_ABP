using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestProj_ABP_Backend.AB_Tests;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Models;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.Controllers;

[Route("experiment/")]
[ApiController]
public class UserController : ControllerBase
{
    IConfiguration _configuration;
    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Get color, based on deviceToken
    /// </summary>
    /// <param name="deviceToken"></param>
    /// <returns>Json key-pair</returns>
    /// <response code="200">Succesfully founded.</response>
    /// <response code="400">User not registered.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpGet("button-color")]
    public IActionResult ButtonColor([FromQuery(Name = "device-token")] string? DeviceToken)
    {
        if (!DeviceToken.IsNullOrEmpty()) DeviceToken = DeviceToken.Trim();
        Result<string> strResult = ColorTest.GetColor(DeviceToken, _configuration);

        if (strResult.IsSuccess == false)
        {
            return BadRequest("device-token wasn't provided");
        }

        var jsonData = new { key = "button-color", value = strResult.Data };
        return new JsonResult(jsonData);
    }


    /// <summary>
    /// Get color, based on fingerprint, or regiter new user, if absent
    /// </summary>
    /// <param name="fingerprintDto"></param>
    /// <returns>Json key-pair</returns>
    /// <response code="200">Succesfully founded.</response>
    /// <response code="400">User not registered.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(201)]
    [HttpPost("get-color-from-fingerprint")]
    public IActionResult GetColorViaFingerprint([FromBody] BrowserFingerprintDto fingerprintDto)
    {
        BrowserFingerprint fingerprint = new(fingerprintDto.DeviceToken, fingerprintDto, HttpContext);

        if (FingerprintService.IsExists(fingerprint, _configuration))
        {
            Result<BrowserFingerprint> res = FingerprintService.IsSimilarToAny(fingerprint, _configuration);

            if (res.IsSuccess)
            {
                var color2 = ColorTest.GetColor(res.Data.DeviceToken, _configuration).Data;
                return Ok(
                    new { key = "button-color", value = color2 }
                    );
            }
        }

        User user = UserService.Register(_configuration);
        ColorTest.AssignColor(user.DeviceToken, _configuration);
        fingerprint.DeviceToken = user.DeviceToken;
        FingerprintService.Register(fingerprint, user, _configuration);
        var color = ColorTest.GetColor(fingerprint.DeviceToken, _configuration).Data;
        return Created(
            Request.GetDisplayUrl(),
            new { key = "button-color", value = color }
            );
    }
}

