using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Services;

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

    public void CreateEconomyPlan(string name, string householdGuid)
    {
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);

        if (household is null)
            return;

        var economyPlan = EconomyPlan.Create(name, _timeService.GetNow());

        _dbContext.Add(economyPlan);

        AddRecurringExpensesToEconomyPlan(economyPlan);
        AddRecurringIncomesToEconomyPlan(economyPlan);

        household.EconomyPlans.Add(economyPlan);

        _dbContext.SaveChanges();
    }

    private void AddRecurringExpensesToEconomyPlan(EconomyPlan economyPlan)
    {
        foreach (var recurringExpense in _dbContext.RecurringExpenses)
        {
            var expense = _mapper.Map<Expense>(recurringExpense);
            expense.RecurringExpense = recurringExpense;

            economyPlan.Expenses.Add(expense);
        }
    }

    private void AddRecurringIncomesToEconomyPlan(EconomyPlan economyPlan)
    {
        foreach (var recurringIncome in _dbContext.RecurringIncomes)
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

    public IEnumerable<EconomyPlanModel> GetActiveEconomyPlansFromHouseholdId(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);
        
        if (household is null)
            throw new InvalidOperationException("GetEconomyPlansFromHouseholdId > Household not found");

        var economyPlans = _dbContext.GetEconomyPlansFromHousehold(household).Where(ep => DateTime.Parse(ep.EndDate) > _timeService.GetNow()).ToList();

        if (!economyPlans.Any())
            throw new InvalidOperationException("GetEconomyPlansFromHouseholdId > No economyplans found");

        foreach (var economyPlan in economyPlans)
        {
            yield return new EconomyPlanModel
                         {
                             ExpenseModels = _mapper.Map<ICollection<ExpenseModel>>(economyPlan.Expenses),
                             IncomeModels = _mapper.Map<ICollection<IncomeModel>>(economyPlan.Incomes),
                             Name = economyPlan.Name,
                             Id = economyPlan.Id,
                             EndDate = economyPlan.EndDate
                         };
        }
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