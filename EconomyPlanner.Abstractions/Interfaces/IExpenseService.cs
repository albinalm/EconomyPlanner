using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IExpenseService
{
    void CreateExpense(ExpenseModel expenseModel);
    void UpdateExpense(Expense expense);
}