using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.API.Bodies;
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

    [HttpPost(Name = "CreateExpense")]
    public IActionResult CreateExpense(CreateExpenseBody expenseBody)
    {
        try
        {
            _expenseService.CreateExpense(expenseBody.EconomyPlanId,
                                          expenseBody.HouseholdGuid,
                                          expenseBody.Name,
                                          expenseBody.Amount,
                                          expenseBody.ExpenseType,
                                          expenseBody.IsRecurring,
                                          expenseBody.RecurringAmount ?? 0);
            return Ok(expenseBody);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "UpdateExpense")]
    public IActionResult UpdateExpense(ExpenseModel expenseModel)
    {
        try
        {
            _expenseService.UpdateExpenseFromModel(expenseModel);
            return Ok(expenseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet(Name = "GetExpense")]
    public IActionResult GetExpense(int expenseId)
    {
        try
        {
            var expenseModel = _expenseService.GetExpenseModel(expenseId);
            
            if (expenseModel is null)
                return NotFound();
            
            return Ok(expenseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet(Name = "GetExpenseTypes")]
    public IActionResult GetExpenseTypes()
    {
        return Ok(_expenseService.GetExpenseTypes());
    }

    [HttpGet(Name = "DeleteExpense")]
    public IActionResult DeleteExpense(int expenseId, bool deleteRecurring)
    {
        _expenseService.DeleteExpense(expenseId, deleteRecurring);
        return Ok();
    }

    [HttpGet(Name = "CheckIfExpenseIsRecurring")]
    public IActionResult CheckIfExpenseIsRecurring(int expenseId)
    {
        return Ok(_expenseService.CheckIfExpenseIsRecurring(expenseId));
    }

    [HttpPost(Name = "CreateRecurringExpense")]
    public IActionResult CreateRecurringExpense(CreateExpenseBody expenseBody)
    {
        _expenseService.CreateRecurringExpense(expenseBody.HouseholdGuid, expenseBody.Name, expenseBody.Amount, expenseBody.ExpenseType);
        return Ok();
    }
    
    [HttpPost(Name = "UpdateRecurringExpense")]
    public IActionResult UpdateRecurringExpense(ExpenseModel expenseModel)
    {
        try
        {
            _expenseService.UpdateRecurringExpenseFromModel(expenseModel);
            return Ok(expenseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet(Name = "DeleteRecurringExpense")]
    public IActionResult DeleteRecurringExpense(int recurringExpenseId)
    {
        _expenseService.DeleteRecurringExpense(recurringExpenseId);
        return Ok();
    }

    [HttpGet(Name = "AddRecurringExpenseAsExpense")]
    public IActionResult AddRecurringExpenseAsExpense(int recurringExpenseId, int economyPlanId)
    {
        _expenseService.AddRecurringExpenseAsExpense(recurringExpenseId, economyPlanId);
        return Ok();
    }

    [HttpGet(Name = "GetAllExpensesLinkedToRecurringExpense")]
    public IActionResult GetAllExpensesLinkedToRecurringExpense(int recurringExpenseId)
    {
        return Ok(_expenseService.GetAllExpenseModelsLinkedToRecurringExpense(recurringExpenseId));
    }
    
    [HttpGet(Name = "GetRecurringExpenseFromExpense")]
    public IActionResult GetRecurringExpenseFromExpense(int expenseId)
    {
        return Ok(_expenseService.GetRecurringExpenseFromExpense(expenseId));
    }
}