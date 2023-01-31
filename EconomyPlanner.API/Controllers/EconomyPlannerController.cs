using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class EconomyPlannerController : ControllerBase
{
    private readonly ILogger<EconomyPlannerController> _logger;
    private readonly IEconomyPlannerService _economyPlannerService;

    public EconomyPlannerController(ILogger<EconomyPlannerController> logger, IEconomyPlannerService economyPlannerService)
    {
        _logger = logger;
        _economyPlannerService = economyPlannerService;
    }

    [HttpPost(Name = "AddEconomyPlan")]
    public IActionResult AddEconomyPlan(EconomyPlanModel economyPlanModel)
    {
        try
        {
            _economyPlannerService.CreateEconomyPlan(economyPlanModel);
            return Ok(economyPlanModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}