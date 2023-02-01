using EconomyPlanner.Repository.Entities.Bases;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class Expense : TransactionBase
{
    public ExpenseType ExpenseType { get; set; }
    public RecurringExpense? RecurringExpense { get; set; }
    public Expense(string name, decimal amount, ExpenseType expenseType, RecurringExpense? recurringExpense) : base(name, amount)
    {
        ExpenseType = expenseType;
        RecurringExpense = recurringExpense;
    }
}