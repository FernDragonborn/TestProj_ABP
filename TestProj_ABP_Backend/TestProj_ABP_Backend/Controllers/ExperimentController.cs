using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestProj_ABP_Backend.AB_Tests;
using TestProj_ABP_Backend.DTOs;
using TestProj_ABP_Backend.Services;

namespace TestProj_ABP_Backend.Controllers;

[Route("experiment/")]
[ApiController]
public class ExperimentController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ExperimentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Get color, based on deviceToken
    /// </summary>
    /// <param name="deviceToken">unique token for each user (device)</param>
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
    /// <param name="fingerprintDto">fingerprint of browser, of user to identify them</param>
    /// <returns>Json key-pair</returns>
    /// <response code="200">Succesfully founded.</response>
    ///  <response code="400">Test started after user registered. Or read message</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpPost("get-color-from-fingerprint")]
    public IActionResult GetColorViaFingerprint([FromBody] BrowserFingerprintDto fingerprintDto)
    {
        Result<string?> res = ColorTest.GetColorViaFingerprint(fingerprintDto, _configuration, HttpContext);

        if (res.IsSuccess)
        {
            return Ok(new { key = "button-color", value = res.Data });
        }
        return BadRequest(res.Message);
    }

    /// <summary>
    /// Get price, based on deviceToken
    /// </summary>
    /// <param name="deviceToken">unique token for each user (device)</param>
    /// <returns>Json key-pair</returns>
    /// <response code="200">Succesfully founded.</response>
    /// <response code="400">User not registered.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpGet("price")]
    public IActionResult Price([FromQuery(Name = "device-token")] string? DeviceToken)
    {
        if (!DeviceToken.IsNullOrEmpty()) DeviceToken = DeviceToken.Trim();
        Result<int?> strResult = PriceTest.GetPrice(DeviceToken, _configuration);

        if (strResult.IsSuccess == false)
        {
            return BadRequest("device-token wasn't provided");
        }

        var jsonData = new { key = "price", value = strResult.Data };
        return new JsonResult(jsonData);
    }

    /// <summary>
    /// Get price, based on fingerprint, or regiter new user, if absent
    /// </summary>
    /// <param name="fingerprintDto">fingerprint of browser, of user to identify them</param>
    /// <returns>Json key-pair</returns>
    /// <response code="200">Succesfully founded.</response>
    ///  <response code="400">Test started after user registered. Or read message</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpPost("get-price-from-fingerprint")]
    public IActionResult GetPriceViaFingerprint([FromBody] BrowserFingerprintDto fingerprintDto)
    {
        Result<int?> res = PriceTest.GetPriceViaFingerprint(fingerprintDto, _configuration, HttpContext);

        if (res.IsSuccess)
        {
            return Ok(new { key = "price", value = res.Data });
        }
        return BadRequest(res.Message);
    }

    [HttpGet("data")]
    [ProducesResponseType(200)]
    public IActionResult GetUserData()
    {
        return Ok(UserService.GetUserData(_configuration));
    }
}

