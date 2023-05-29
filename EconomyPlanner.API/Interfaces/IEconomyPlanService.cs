using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API.Interfaces;

public interface IEconomyPlanService
{
    void CreateEconomyPlan(string name, string householdGuid, DateTime period);
    void RemoveExpense(int economyPlanId, int expenseId, bool removeRecurring);
    IEnumerable<EconomyPlan> GetEconomyPlansFromHouseholdId(string guid);
    void RemoveIncome(int economyPlanId, int incomeId, bool removeRecurring);
    EconomyPlan? GetEconomyPlan(int economyPlanId);
    EconomyPlan? GetEconomyPlanByDate(DateTime startPeriod);
}