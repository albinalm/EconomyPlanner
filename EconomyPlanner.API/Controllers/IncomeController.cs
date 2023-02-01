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
            _incomeService.CreateIncome(incomeBody.EconomyPlanId, incomeBody.Name, incomeBody.Amount, incomeBody.IncomeTypeId, incomeBody.IsRecurring);
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
}