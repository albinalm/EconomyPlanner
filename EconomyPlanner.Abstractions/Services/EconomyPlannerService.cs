using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Helpers;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Abstractions.Services;

public class EconomyPlannerService : IEconomyPlannerService
{
    private readonly DatabaseContext _dbContext;
    private readonly IEconomyPlanManager _economyPlanManager;

    public EconomyPlannerService(DatabaseContext dbContext, IEconomyPlanManager economyPlanManager)
    {
        _dbContext = dbContext;
        _economyPlanManager = economyPlanManager;
    }

    public void CreateEconomyPlan(EconomyPlanModel economyPlanModel)
    {
        var economyPlan = _economyPlanManager.Create(economyPlanModel.Name, 
                                                     economyPlanModel.Expenses, 
                                                     economyPlanModel.Incomes);
        _dbContext.Add(economyPlan);
        _dbContext.SaveChanges();
    }
    
    public void AddExpense(EconomyPlan economyPlan, Expense expense)
    {
        economyPlan.Expenses.Add(expense);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();
    }
    
    public void DeleteExpense(EconomyPlan economyPlan, Expense expense)
    {
        if (economyPlan.Expenses.Contains(expense))
            economyPlan.Expenses.Remove(expense);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();  
    }
    
    public void AddIncome(EconomyPlan economyPlan, Income income)
    {
        economyPlan.Incomes.Add(income);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();
    }
    
    public void DeleteIncome(EconomyPlan economyPlan, Income income)
    {
        if (economyPlan.Incomes.Contains(income))
            economyPlan.Incomes.Remove(income);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();  
    }

    public EconomyPlan? GetEconomyPlanByDate(DateTime startPeriod)
    {
        return _dbContext.EconomyPlans.FirstOrDefault(ep => ep.StartDate == startPeriod.ToString("yyyy-MM-dd"));
    }
}