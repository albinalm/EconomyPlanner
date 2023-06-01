using AutoMapper;
using EconomyPlanner.API.Helpers;
using EconomyPlanner.API.Interfaces;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyPlanner.API.Services;

public class EconomyPlanService : IEconomyPlanService
{
    private readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ITimeService _timeService;

    public EconomyPlanService(DatabaseContext dbContext, IMapper mapper, ITimeService timeService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _timeService = timeService;
    }

    public void CreateEconomyPlan(string name, string householdGuid, DateTime period)
    {
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);

        if (household is null)
            return;

        var economyPlan = EconomyPlan.Create(name, period);

        _dbContext.Add(economyPlan);

        AddRecurringExpensesToEconomyPlan(economyPlan);
        AddRecurringIncomesToEconomyPlan(economyPlan);

        household.EconomyPlans.Add(economyPlan);

        _dbContext.SaveChanges();
    }

    private void AddRecurringExpensesToEconomyPlan(EconomyPlan economyPlan)
    {
        var recurringExpenses = _dbContext.Households
                                          .Include(h => h.RecurringExpenses)
                                          .Include(h => h.EconomyPlans)
                                          .SingleOrDefault(x => x.EconomyPlans.Contains(economyPlan))?.RecurringExpenses;
        
        if (recurringExpenses is null)
            return;
        
        foreach (var recurringExpense in recurringExpenses)
        {
            var expense = _mapper.Map<Expense>(recurringExpense);
            expense.RecurringExpense = recurringExpense;

            economyPlan.Expenses.Add(expense);
        }
    }

    private void AddRecurringIncomesToEconomyPlan(EconomyPlan economyPlan)
    {
        var recurringIncomes = _dbContext.Households
                                          .Include(h => h.RecurringIncomes)
                                          .Include(h => h.EconomyPlans)
                                          .SingleOrDefault(x => x.EconomyPlans.Contains(economyPlan))?.RecurringIncomes;
        
        if (recurringIncomes is null)
            return;
        
        foreach (var recurringIncome in recurringIncomes)
        {
            var income = _mapper.Map<Income>(recurringIncome);
            income.RecurringIncome = recurringIncome;

            economyPlan.Incomes.Add(income);
        }
    }
    
    public void RemoveExpense(int economyPlanId, int expenseId, bool removeRecurring)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var expense = _dbContext.GetExpenseFromId(expenseId);

        if (economyPlan is null || expense is null)
            return;

        if (economyPlan.Expenses.Contains(expense))
            economyPlan.Expenses.Remove(expense);

        if (removeRecurring && expense.RecurringExpense is not null)
        {
            var recurringExpense = _dbContext.GetRecurringExpenseFromId(expense.RecurringExpense.Id);

            if (recurringExpense is null)
                return;

            _dbContext.Remove(recurringExpense);
        }

        _dbContext.Remove(expense);
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();
    }

    public IEnumerable<EconomyPlan> GetEconomyPlansFromHouseholdId(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);
        
        if (household is null)
            throw new InvalidOperationException("GetEconomyPlansFromHouseholdId > Household not found");

        var economyPlans = _dbContext.GetEconomyPlansFromHousehold(household)?.ToList();
        
        return economyPlans ?? Enumerable.Empty<EconomyPlan>();
    }
    
    public void RemoveIncome(int economyPlanId, int incomeId, bool removeRecurring)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var income = _dbContext.GetIncomeFromId(incomeId);

        if (economyPlan is null || income is null)
            return;

        if (economyPlan.Incomes.Contains(income))
            economyPlan.Incomes.Remove(income);

        if (removeRecurring && income.RecurringIncome is not null)
        {
            var recurringIncome = _dbContext.GetRecurringIncomeFromId(income.RecurringIncome.Id);

            if (recurringIncome is null)
                return;

            _dbContext.Remove(recurringIncome);
        }

        _dbContext.Remove(income);
        _dbContext.Update(economyPlan);
        _dbContext.SaveChanges();
    }

    public EconomyPlan? GetEconomyPlan(int economyPlanId) => _dbContext.GetEconomyPlanFromId(economyPlanId);

    public EconomyPlan? GetEconomyPlanByDate(DateTime startPeriod)
    {
        return _dbContext.EconomyPlans.FirstOrDefault(ep => ep.StartDate == startPeriod.ToString("yyyy-MM-dd"));
    }

    public void SetupActiveEconomyPlans(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);
        
        if (household is null)
            throw new InvalidOperationException("SetupActiveEconomyPlans > Household not found");

        var currentTime = _timeService.GetNow();
        
        var activeEconomyPlans = GetEconomyPlansFromHouseholdId(guid).Where(ep => currentTime.Month <= DateTime.Parse(ep.EndDate).Month).ToList();

        if (activeEconomyPlans.Count == 2)
        {
            return;
        }

        if (activeEconomyPlans.Count == 1)
        {
            var month = currentTime.Month;
            var economyPlanMonth = DateTime.Parse(activeEconomyPlans.Single().EndDate).Month;

            if (month != economyPlanMonth)
            {
                var period = currentTime.AddMonths(1);
                CreateEconomyPlan(EconomyPlanHelper.GetEconomyPlanName(period), guid, period);
            }
            else
            {
                CreateEconomyPlan(EconomyPlanHelper.GetEconomyPlanName(currentTime), guid, currentTime);
            }
        }
        
        else if (!activeEconomyPlans.Any())
        {
            var futureMonthDate = currentTime.AddMonths(1);
            CreateEconomyPlan(EconomyPlanHelper.GetEconomyPlanName(currentTime), household.Guid, currentTime);
            CreateEconomyPlan(EconomyPlanHelper.GetEconomyPlanName(futureMonthDate), household.Guid, futureMonthDate);
        }
    }

    public IEnumerable<EconomyPlan> TryGetOneYearEconomyPlans(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);
        
        if (household is null)
            throw new InvalidOperationException("EconomyPlanService > TryGetOneYearEconomyPlans Household not found");
        
        var economyPlans = _dbContext.GetEconomyPlansFromHousehold(household)?.ToList();
        
        if (economyPlans is null || !economyPlans.Any())
        {
            return Enumerable.Empty<EconomyPlan>();
        }

        var latestEconomyPlans = economyPlans.OrderBy(ep => DateTime.Parse(ep.EndDate)).TakeLast(12).ToList();
        
        return latestEconomyPlans;
    }
}