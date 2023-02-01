﻿namespace EconomyPlanner.API.Bodies;

public class CreateIncomeBody
{
    public int EconomyPlanId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int IncomeTypeId { get; set; }
    public bool IsRecurring { get; set; }
    public decimal? RecurringAmount { get; set; }
}