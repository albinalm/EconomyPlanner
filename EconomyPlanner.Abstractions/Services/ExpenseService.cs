using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Services;

public class ExpenseService : IExpenseService
{
    private readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseService(DatabaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void CreateExpense(int economyPlanId, string name, decimal amount, int expenseTypeId, bool isRecurring)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);

        if (economyPlan is null)
            return;

        var expense = Expense.Create(name, amount, (ExpenseType)expenseTypeId, null);

        if (isRecurring)
        {
            var recurringExpense = RecurringExpense.Create(name, amount, (ExpenseType)expenseTypeId);
                
            _dbContext.Add(recurringExpense);
                
            expense.SetRecurringExpense(recurringExpense);
        }

        _dbContext.Expenses.Add(expense);
        economyPlan.Expenses.Add(expense);
        _dbContext.EconomyPlans.Update(economyPlan);
        _dbContext.SaveChanges();
    }

    public void UpdateExpenseFromModel(ExpenseModel expenseModel)
    {
        var expense = _dbContext.GetExpenseFromId(expenseModel.Id);
        
        if (expense is null)
            return;

        _mapper.Map(expenseModel, expense);

        _dbContext.Update(expense);
        _dbContext.SaveChanges();
    }

    public ExpenseModel? GetExpenseModel(int expenseId)
    {
        var expense = _dbContext.Expenses.Find(expenseId);
        
        return expense is not null ? _mapper.Map<ExpenseModel>(expense) : null;
    }
}