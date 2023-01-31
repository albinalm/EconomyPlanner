using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Managers.Interfaces;

public interface IExpenseManager
{
    Expense Create(string name, decimal amount, ExpenseType expenseType, bool recurring, decimal? recurringAmount);
}