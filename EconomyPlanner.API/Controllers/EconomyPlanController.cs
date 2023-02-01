﻿using EconomyPlanner.Abstractions.Interfaces;
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

    [HttpPost(Name = "CreateEconomyPlan")]
    public IActionResult CreateEconomyPlan(string name)
    {
        try
        {
            _economyPlannerService.CreateEconomyPlan(name);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "AddExpense")]
    public IActionResult AddExpense(int economyPlanId, int expenseId)
    {
        try
        {
            _economyPlannerService.AddExpense(economyPlanId, expenseId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "AddIncome")]
    public IActionResult AddIncome(int economyPlanId, int incomeId)
    {
        try
        {
            _economyPlannerService.AddIncome(economyPlanId, incomeId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "RemoveExpense")]
    public IActionResult RemoveExpense(int economyPlanId, int expenseId)
    {
        try
        {
            _economyPlannerService.RemoveExpense(economyPlanId, expenseId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "RemoveIncome")]
    public IActionResult RemoveIncome(int economyPlanId, int incomeId)
    {
        try
        {
            _economyPlannerService.RemoveIncome(economyPlanId, incomeId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}