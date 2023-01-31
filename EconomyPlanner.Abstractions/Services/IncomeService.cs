using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Abstractions.Services;

public class IncomeService : IIncomeService
{
    private readonly DatabaseContext _dbContext;
    private readonly IIncomeManager _incomeManager;

    public IncomeService(DatabaseContext dbContext, IIncomeManager incomeManager)
    {
        _dbContext = dbContext;
        _incomeManager = incomeManager;
    }

    public void CreateIncome(IncomeModel incomeModel)
    {
        _dbContext.Add(_incomeManager.Create(incomeModel.Name, incomeModel.Amount, incomeModel.IncomeType, incomeModel.Recurring, incomeModel.RecurringAmount));
        _dbContext.SaveChanges();
    }
    
    public void UpdateIncome(Income income)
    {
        _dbContext.Update(income);
        _dbContext.SaveChanges();
    }
}