using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Models;

public class ExpenseModel
{
    public ExpenseModel(string name, decimal amount, ExpenseType expenseType, bool recurring, decimal? recurringAmount)
    {
        Name = name;
        Amount = amount;
        ExpenseType = expenseType;
        Recurring = recurring;
        RecurringAmount = recurringAmount;
    }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public bool Recurring { get; set; }
    public decimal? RecurringAmount { get; set; }
    public bool IsDeleted { get; set; }
}