using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
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

    [HttpPost(Name = "AddIncome")]
    public IActionResult AddIncome(IncomeModel incomeModel)
    {
        try
        {
            _incomeService.CreateIncome(incomeModel);
            return Ok(incomeModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}