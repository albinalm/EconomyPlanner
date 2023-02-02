using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IEconomyPlannerService
{
    void CreateEconomyPlan(string name, string householdGuid);
    // void AddExpense(int economyPlanId, int expenseId);
    void RemoveExpense(int economyPlanId, int expenseId, bool removeRecurring);
    // void AddIncome(int economyPlanId, int incomeId);
    void RemoveIncome(int economyPlanId, int incomeId, bool removeRecurring);
    EconomyPlan? GetEconomyPlanByDate(DateTime startPeriod);
    IEnumerable<EconomyPlanModel> GetEconomyPlansFromHouseholdId(string guid);
}