using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Helpers;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Abstractions.Services;

public class EconomyPlanService : IEconomyPlannerService
{
    private readonly DatabaseContext _dbContext;
    private readonly IEconomyPlanManager _economyPlanManager;
    private readonly IMapper _mapper;

    public EconomyPlanService(DatabaseContext dbContext, IEconomyPlanManager economyPlanManager, IMapper mapper)
    {
        _dbContext = dbContext;
        _economyPlanManager = economyPlanManager;
        _mapper = mapper;
    }

    public void CreateEconomyPlan(string name)
    {
        var economyPlan = _economyPlanManager.Create(name);
        _dbContext.Add(economyPlan);
        _dbContext.SaveChanges();
    }
    
    public void AddExpense(int economyPlanId, int expenseId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var expense = _dbContext.GetExpenseFromId(expenseId);
        
        if (economyPlan is null || expense is null)
            return;
        
        economyPlan.Expenses.Add(expense);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();
    }
    
    public void RemoveExpense(int economyPlanId, int expenseId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var expense = _dbContext.GetExpenseFromId(expenseId);
        
        if (economyPlan is null || expense is null)
            return;
        
        if (economyPlan.Expenses.Contains(expense))
            economyPlan.Expenses.Remove(expense);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();  
    }
    
    public void AddIncome(int economyPlanId, int incomeId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var income = _dbContext.GetIncomeFromId(incomeId);
        
        if (economyPlan is null || income is null)
            return;
        
        economyPlan.Incomes.Add(income);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();
    }
    
    public void RemoveIncome(int economyPlanId, int incomeId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var income = _dbContext.GetIncomeFromId(incomeId);
        
        if (economyPlan is null || income is null)
            return;
        
        if (economyPlan.Incomes.Contains(income))
            economyPlan.Incomes.Remove(income);
        
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();  
    }

    public EconomyPlanModel? GetEconomyPlan(int economyPlanId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);

        return economyPlan is not null ? _mapper.Map<EconomyPlanModel>(economyPlan) : null;
    }
    
    public EconomyPlan? GetEconomyPlanByDate(DateTime startPeriod)
    {
        return _dbContext.EconomyPlans.FirstOrDefault(ep => ep.StartDate == startPeriod.ToString("yyyy-MM-dd"));
    }
}