using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizBulbApi.Models;
using WizBulbApi.Services;

namespace WizBulbApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WizBulbController : ControllerBase
{
    private readonly IWizBulbService _wizBulbService;

    public WizBulbController(IWizBulbService wizBulbService)
    {
        _wizBulbService = wizBulbService;
    }

    [Authorize]
    [HttpGet("discover")]
    public async Task<IActionResult> DiscoverBulbs()
    {
        var result = await _wizBulbService.DiscoverBulbsAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpPost("setPilot")]
    public async Task<IActionResult> SetPilot([FromQuery] string ipAddress, [FromBody] WizBulbPilot pilot)
    {
        var result = await _wizBulbService.SetPilotAsync(ipAddress, pilot);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getPilot")]
    public async Task<IActionResult> GetPilot([FromQuery] string ipAddress)
    {
        var result = await _wizBulbService.GetPilotAsync(ipAddress);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("setSystemConfig")]
    public async Task<IActionResult> SetSystemConfig([FromQuery] string ipAddress, [FromBody] WizBulbConfig config)
    {
        var result = await _wizBulbService.SetSystemConfigAsync(ipAddress, config);
        return Ok(result);
    }
}