using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class RecurringExpense : TransactionBase
{
    public string ExpenseType { get; set; }
    
    public RecurringExpense(string name, decimal amount, string expenseType) : base(name, amount)
    {
        ExpenseType = expenseType;
    }
    
    public static RecurringExpense Create(string name, decimal amount, string expenseType)
    {
        return new RecurringExpense(name, amount, expenseType);
    }
}