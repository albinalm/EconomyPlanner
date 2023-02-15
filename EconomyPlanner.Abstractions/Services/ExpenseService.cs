using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.TransactionTypes;
using Microsoft.EntityFrameworkCore;

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

    public void CreateExpense(int economyPlanId, string householdGuid, string name, decimal amount, string expenseType, bool isRecurring, decimal recurringAmount)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);
        
        if (economyPlan == null || household == null)
            return;

        var expense = Expense.Create(name, amount, expenseType, null);

        if (isRecurring)
        {
            var recurringExpense = RecurringExpense.Create(name, recurringAmount, expenseType);

            _dbContext.Add(recurringExpense);
            
            household.RecurringExpenses.Add(recurringExpense);
            expense.SetRecurringExpense(recurringExpense);
        }

        _dbContext.Expenses.Add(expense);
        economyPlan.Expenses.Add(expense);
        _dbContext.EconomyPlans.Update(economyPlan);
        _dbContext.Households.Update(household);
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

    public IEnumerable<string> GetExpenseTypes()
    {
        return ExpenseType.GetExpenseTypes();
    }
    
    public void DeleteExpense(int expenseId, bool deleteRecurring)
    {
        var expense = _dbContext.Expenses.Where(e => e.Id == expenseId)
                                .Include(e => e.RecurringExpense)
                                .FirstOrDefault();

        if (expense is null)
            return;
        
        if (deleteRecurring && expense.RecurringExpense != null)
            _dbContext.RecurringExpenses.Remove(expense.RecurringExpense);

        _dbContext.Expenses.Remove(expense);
        _dbContext.SaveChanges();
    }
    
    public bool CheckIfExpenseIsRecurring(int expenseId)
    {
        var expense = _dbContext.Expenses.Where(e => e.Id == expenseId)
                                .Include(e => e.RecurringExpense)
                                .FirstOrDefault();
        
        return expense?.RecurringExpense != null;
    }
}