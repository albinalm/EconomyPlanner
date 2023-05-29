using EconomyPlanner.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class EconomyPlanController : ControllerBase
{
    private readonly ILogger<EconomyPlanController> _logger;
    private readonly IEconomyPlanService _economyPlanService;

    public EconomyPlanController(ILogger<EconomyPlanController> logger, IEconomyPlanService economyPlanService)
    {
        _logger = logger;
        _economyPlanService = economyPlanService;
    }

    [HttpPost(Name = "CreateEconomyPlan")]
    public IActionResult CreateEconomyPlan(string name, string householdGuid)
    {
        try
        {
            _economyPlanService.CreateEconomyPlan(name, householdGuid);
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
            _economyPlanService.RemoveExpense(economyPlanId, expenseId, removeRecurring);
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
            _economyPlanService.RemoveIncome(economyPlanId, incomeId, removeRecurring);
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
            return Ok(_economyPlanService.GetActiveEconomyPlansFromHouseholdId(guid).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}