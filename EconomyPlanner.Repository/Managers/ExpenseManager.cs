using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Repository.Managers;

public class ExpenseManager : IExpenseManager
{
    public Expense Create(string name, decimal amount, ExpenseType expenseType, bool recurring, decimal? recurringAmount)
    {
        if (recurring && recurringAmount is null)
            throw new InvalidOperationException("Recurring amount must have a value if recurring is set to true");
        
        return new Expense(name, amount, expenseType, recurring, recurringAmount);
    }
}