using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IExpenseService
{
    void CreateExpense(int economyPlanId, string name, decimal amount, int expenseTypeId, bool isRecurring, decimal? recurringAmount);
    void UpdateExpense(ExpenseModel expenseModel);
    ExpenseModel? GetExpenseModel(int expenseId);
}