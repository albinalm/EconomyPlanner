using EconomyPlanner.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]

public class TimeController : ControllerBase
{
    private readonly ITimeService _timeService;

    public TimeController(ITimeService timeService)
    {
        _timeService = timeService;
    }

    [HttpGet(Name = "GetNow")]
    public IActionResult GetNow()
    {
        return Ok(_timeService.GetNow());
    }
}