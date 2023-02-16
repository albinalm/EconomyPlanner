using EconomyPlanner.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class EconomyPlanController : ControllerBase
{
    private readonly ILogger<EconomyPlanController> _logger;
    private readonly IEconomyPlannerService _economyPlannerService;

    public EconomyPlanController(ILogger<EconomyPlanController> logger, IEconomyPlannerService economyPlannerService)
    {
        _logger = logger;
        _economyPlannerService = economyPlannerService;
    }

    [HttpPost(Name = "CreateEconomyPlan")]
    public IActionResult CreateEconomyPlan(string name, string householdGuid)
    {
        try
        {
            _economyPlannerService.CreateEconomyPlan(name, householdGuid);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost(Name = "RemoveExpense")]
    public IActionResult RemoveExpense(int economyPlanId, int expenseId, bool removeRecurring)
    {
        try
        {
            _economyPlannerService.RemoveExpense(economyPlanId, expenseId, removeRecurring);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "RemoveIncome")]
    public IActionResult RemoveIncome(int economyPlanId, int incomeId, bool removeRecurring)
    {
        try
        {
            _economyPlannerService.RemoveIncome(economyPlanId, incomeId, removeRecurring);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet(Name = "GetActiveEconomyPlansFromHouseholdGuid")]
    public IActionResult GetActiveEconomyPlansFromHouseholdGuid(string guid)
    {
        try
        {
            return Ok(_economyPlannerService.GetActiveEconomyPlansFromHouseholdId(guid).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}