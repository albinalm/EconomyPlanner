using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.API.Bodies;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class IncomeController : ControllerBase
{
    private readonly ILogger<IncomeController> _logger;
    private readonly IIncomeService _incomeService;

    public IncomeController(ILogger<IncomeController> logger, IIncomeService incomeService)
    {
        _logger = logger;
        _incomeService = incomeService;
    }

    [HttpPost(Name = "CreateIncome")]
    public IActionResult CreateIncome(CreateIncomeBody incomeBody)
    {
        try
        {
            _incomeService.CreateIncome(incomeBody.EconomyPlanId,
                                        incomeBody.HouseholdGuid,
                                        incomeBody.Name,
                                        incomeBody.Amount,
                                        incomeBody.IncomeType,
                                        incomeBody.IsRecurring,
                                        incomeBody.RecurringAmount ?? 0);
            return Ok(incomeBody);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost(Name = "UpdateIncome")]
    public IActionResult UpdateIncome(IncomeModel incomeModel)
    {
        try
        {
            _incomeService.UpdateIncomeFromModel(incomeModel);
            return Ok(incomeModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet(Name = "GetIncome")]
    public IActionResult GetIncome(int incomeId)
    {
        try
        {
            var incomeModel = _incomeService.GetIncomeModel(incomeId);

            if (incomeModel is null)
                return NotFound();

            return Ok(incomeModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet(Name = "GetIncomeTypes")]
    public IActionResult GetIncomeTypes()
    {
        return Ok(_incomeService.GetIncomeTypes());
    }

    [HttpGet(Name = "DeleteIncome")]
    public IActionResult DeleteIncome(int incomeId, bool deleteRecurring)
    {
        _incomeService.DeleteIncome(incomeId, deleteRecurring);
        return Ok();
    }

    [HttpGet(Name = "CheckIfIncomeIsRecurring")]
    public IActionResult CheckIfIncomeIsRecurring(int incomeId)
    {
        return Ok(_incomeService.CheckIfIncomeIsRecurring(incomeId));
    }

    [HttpPost(Name = "CreateRecurringIncome")]
    public IActionResult CreateRecurringIncome(CreateIncomeBody incomeBody)
    {
        _incomeService.CreateRecurringIncome(incomeBody.HouseholdGuid, incomeBody.Name, incomeBody.Amount, incomeBody.IncomeType);
        return Ok();
    }

    [HttpPost(Name = "UpdateRecurringIncome")]
    public IActionResult UpdateRecurringIncome(IncomeModel incomeModel)
    {
        try
        {
            _incomeService.UpdateRecurringIncomeFromModel(incomeModel);
            return Ok(incomeModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet(Name = "DeleteRecurringIncome")]
    public IActionResult DeleteRecurringIncome(int recurringIncomeId)
    {
        _incomeService.DeleteRecurringIncome(recurringIncomeId);
        return Ok();
    }

    [HttpGet(Name = "AddRecurringIncomeAsIncome")]
    public IActionResult AddRecurringIncomeAsIncome(int recurringIncomeId, int economyPlanId)
    {
        _incomeService.AddRecurringIncomeAsIncome(recurringIncomeId, economyPlanId);
        return Ok();
    }

    [HttpGet(Name = "GetAllIncomesLinkedToRecurringIncome")]
    public IActionResult GetAllIncomesLinkedToRecurringIncome(int recurringIncomeId)
    {
        return Ok(_incomeService.GetAllIncomeModelsLinkedToRecurringIncome(recurringIncomeId));
    }

    [HttpGet(Name = "GetRecurringIncomeFromIncome")]
    public IActionResult GetRecurringIncomeFromIncome(int incomeId)
    {
        return Ok(_incomeService.GetRecurringIncomeFromIncome(incomeId));
    }
}