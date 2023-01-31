using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Abstractions.Services;

public class ExpenseService : IExpenseService
{
    private readonly DatabaseContext _dbContext;
    private readonly IExpenseManager _expenseManager;

    public ExpenseService(DatabaseContext dbContext, IExpenseManager expenseManager)
    {
        _dbContext = dbContext;
        _expenseManager = expenseManager;
    }

    public void CreateExpense(ExpenseModel expenseModel)
    {
        _dbContext.Add(_expenseManager.Create(expenseModel.Name, expenseModel.Amount, expenseModel.ExpenseType, expenseModel.Recurring, expenseModel.RecurringAmount));
        _dbContext.SaveChanges();
    }

    public void UpdateExpense(Expense expense)
    {
        _dbContext.Update(expense);
        _dbContext.SaveChanges();
    }
}