using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public bool Recurring { get; set; }
    public decimal? RecurringAmount { get; set; }
    public bool IsDeleted { get; set; }
    
    public Expense(string name, decimal amount, ExpenseType expenseType, bool recurring, decimal? recurringAmount)
    {
        Name = name;
        Amount = amount;
        ExpenseType = expenseType;
        Recurring = recurring;
        RecurringAmount = recurringAmount;
    }

}