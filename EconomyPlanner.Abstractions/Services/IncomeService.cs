using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;

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

    public void CreateIncome(int economyPlanId, string householdGuid, string name, decimal amount, string incomeType, bool isRecurring)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);
        
        if (economyPlan == null || household == null)
            return;

        var income = Income.Create(name, amount, incomeType, null);

        if (isRecurring)
        {
            var recurringIncome = RecurringIncome.Create(name, amount, incomeType);

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