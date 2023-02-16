using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.TransactionTypes;
using Microsoft.EntityFrameworkCore;

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

    public void CreateIncome(int economyPlanId,
                             string householdGuid,
                             string name,
                             decimal amount,
                             string incomeType,
                             bool isRecurring,
                             decimal recurringAmount)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);

        if (economyPlan == null || household == null)
            return;

        var income = Income.Create(name, amount, incomeType, null);

        if (isRecurring)
        {
            var recurringIncome = RecurringIncome.Create(name, recurringAmount, incomeType);

            _dbContext.Add(recurringIncome);

            household.RecurringIncomes.Add(recurringIncome);
            income.SetRecurringIncome(recurringIncome);
        }

        _dbContext.Incomes.Add(income);
        economyPlan.Incomes.Add(income);
        _dbContext.EconomyPlans.Update(economyPlan);
        _dbContext.Households.Update(household);
        _dbContext.SaveChanges();
    }

    public void CreateRecurringIncome(string householdGuid, string name, decimal amount, string incomeType)
    {
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);

        if (household == null)
            return;

        var recurringIncome = RecurringIncome.Create(name, amount, incomeType);

        _dbContext.Add(recurringIncome);

        household.RecurringIncomes.Add(recurringIncome);

        _dbContext.Households.Update(household);
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

    public void UpdateRecurringIncomeFromModel(IncomeModel incomeModel)
    {
        var recurringIncome = _dbContext.GetRecurringIncomeFromId(incomeModel.Id);

        if (recurringIncome is null)
            return;

        _mapper.Map(incomeModel, recurringIncome);

        _dbContext.Update(recurringIncome);
        _dbContext.SaveChanges();
    }

    public IncomeModel? GetIncomeModel(int incomeId)
    {
        var income = _dbContext.Incomes.Find(incomeId);

        return income is not null ? _mapper.Map<IncomeModel>(income) : null;
    }

    public IEnumerable<string> GetIncomeTypes()
    {
        return IncomeType.GetIncomeTypes();
    }

    public void DeleteIncome(int incomeId, bool deleteRecurring)
    {
        var income = _dbContext.Incomes.Where(e => e.Id == incomeId)
                               .Include(e => e.RecurringIncome)
                               .FirstOrDefault();

        if (income is null)
            return;

        if (deleteRecurring && income.RecurringIncome != null)
        {
            var recurringIncome = income.RecurringIncome;

            var linkedIncomes = GetAllIncomesLinkedToRecurringIncome(income.RecurringIncome);
            if (linkedIncomes.Any())
            {
                foreach (var linkedIncome in linkedIncomes)
                {
                    linkedIncome.RecurringIncome = null;
                }
            }

            _dbContext.RecurringIncomes.Remove(recurringIncome);
        }


        _dbContext.Incomes.Remove(income);
        _dbContext.SaveChanges();
    }

    public void DeleteRecurringIncome(int recurringIncomeId)
    {
        var recurringIncome = _dbContext.RecurringIncomes.Find(recurringIncomeId);

        if (recurringIncome is null)
            return;

        var linkedIncomes = GetAllIncomesLinkedToRecurringIncome(recurringIncome);
        if (linkedIncomes.Any())
        {
            foreach (var linkedIncome in linkedIncomes)
            {
                linkedIncome.RecurringIncome = null;
            }
        }

        _dbContext.RecurringIncomes.Remove(recurringIncome);
        _dbContext.SaveChanges();
    }

    private List<Income> GetAllIncomesLinkedToRecurringIncome(RecurringIncome recurringIncome)
    {
        return _dbContext.Incomes.Where(e => e.RecurringIncome == recurringIncome).ToList();
    }

    public bool CheckIfIncomeIsRecurring(int incomeId)
    {
        var income = _dbContext.Incomes.Where(e => e.Id == incomeId)
                               .Include(e => e.RecurringIncome)
                               .FirstOrDefault();

        return income?.RecurringIncome != null;
    }

    public void AddRecurringIncomeAsIncome(int recurringIncomeId, int economyPlanId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var recurringIncome = _dbContext.RecurringIncomes.Find(recurringIncomeId);

        if (economyPlan == null || recurringIncome == null)
            return;

        var income = Income.Create(recurringIncome.Name, recurringIncome.Amount, recurringIncome.IncomeType, null);
        income.RecurringIncome = recurringIncome;

        _dbContext.Incomes.Add(income);
        economyPlan.Incomes.Add(income);
        _dbContext.EconomyPlans.Update(economyPlan);
        _dbContext.SaveChanges();
    }

    public IncomeModel GetRecurringIncomeFromIncome(int incomeId)
    {
        return _mapper.Map<IncomeModel>(_dbContext.Incomes.Where(e => e.Id == incomeId)
                                                  .Include(e => e.RecurringIncome)
                                                  .FirstOrDefault()
                                                  ?.RecurringIncome);
    }
    
    public IEnumerable<IncomeModel> GetAllIncomeModelsLinkedToRecurringIncome(int recurringIncomeId)
    {
        var recurringIncome = _dbContext.RecurringIncomes.Find(recurringIncomeId);
        return _mapper.Map<IEnumerable<IncomeModel>>(_dbContext.Incomes.Where(e => e.RecurringIncome == recurringIncome).ToList());
    }
}