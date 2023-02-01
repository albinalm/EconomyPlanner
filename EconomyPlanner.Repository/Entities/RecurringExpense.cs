using EconomyPlanner.Repository.Entities.Bases;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class RecurringExpense : TransactionBase
{
    public ExpenseType ExpenseType { get; set; }
    
    public RecurringExpense(string name, decimal amount, ExpenseType expenseType) : base(name, amount)
    {
        ExpenseType = expenseType;
    }
    
    public static RecurringExpense Create(string name, decimal amount, ExpenseType expenseType)
    {
        return new RecurringExpense(name, amount, expenseType);
    }
}