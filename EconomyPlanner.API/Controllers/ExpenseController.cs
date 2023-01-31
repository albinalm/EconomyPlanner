using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class ExpenseController : ControllerBase
{
    private readonly ILogger<ExpenseController> _logger;
    private readonly IExpenseService _expenseService;

    public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService)
    {
        _logger = logger;
        _expenseService = expenseService;
    }

    [HttpPost(Name = "AddExpense")]
    public IActionResult AddExpense(ExpenseModel expenseModel)
    {
        try
        {
            _expenseService.CreateExpense(expenseModel);
            return Ok(expenseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}