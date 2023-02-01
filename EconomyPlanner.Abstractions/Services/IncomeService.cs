using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Services;

public class IncomeService : IIncomeService
{
    private readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;

    public IncomeService(DatabaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void CreateIncome(int economyPlanId, string name, decimal amount, int incomeTypeId, bool isRecurring)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);

            if (economyPlan is null)
                return;
            
            var income = Income.Create(name, amount, (IncomeType)incomeTypeId, null);
            
            if (isRecurring)
            {
                var recurringIncome = RecurringIncome.Create(name, amount, (IncomeType)incomeTypeId);
                
                _dbContext.Add(recurringIncome);
                
                income.SetRecurringIncome(recurringIncome);
            }
            
            _dbContext.Incomes.Add(income);
            economyPlan.Incomes.Add(income);
            _dbContext.EconomyPlans.Update(economyPlan);
            _dbContext.SaveChanges();
    }
    
    public void UpdateIncomeFromModel(IncomeModel incomeModel)
    {
        var income = _dbContext.GetIncomeFromId(incomeModel.Id);
        
        if (income is null)
            return;

        _mapper.Map(incomeModel, income);

        _dbContext.Update(income);
        _dbContext.SaveChanges();
    }
    
    public IncomeModel? GetIncomeModel(int incomeId)
    {
        var income = _dbContext.Expenses.Find(incomeId);
        
        return income is not null ? _mapper.Map<IncomeModel>(income) : null;
    }
}