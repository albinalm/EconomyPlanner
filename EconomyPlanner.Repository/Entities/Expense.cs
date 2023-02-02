using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class Expense : TransactionBase
{
    public string ExpenseType { get; set; }
    public RecurringExpense? RecurringExpense { get; set; }
    public Expense(string name, decimal amount, string expenseType) : base(name, amount)
    {
        ExpenseType = expenseType;
    }
    
    public static Expense Create(string name, decimal amount, string expenseType, RecurringExpense? recurringExpense)
    {
        return new Expense(name, amount, expenseType);
    }
    
    public void SetRecurringExpense(RecurringExpense? recurringExpense) => RecurringExpense = recurringExpense;
}