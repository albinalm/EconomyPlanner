using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public decimal RecurringAmount { get; set; }
    public bool Recurring { get; set; }
}