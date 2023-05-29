using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API.Services;

public interface IIncomeService
{
    void CreateIncome(int economyPlanId,
                      string householdGuid,
                      string name,
                      decimal amount,
                      string incomeType,
                      bool isRecurring,
                      decimal recurringAmount);

    void CreateRecurringIncome(string householdGuid, string name, decimal amount, string incomeType);
    void UpdateIncome(Income income);
    void UpdateRecurringIncome(RecurringIncome recurringIncome);
    Income? GetIncome(int incomeId);
    IEnumerable<string> GetIncomeTypes();
    void DeleteIncome(int incomeId, bool deleteRecurring);
    void DeleteRecurringIncome(int recurringIncomeId);
    IEnumerable<Income> GetAllIncomesLinkedToRecurringIncome(int recurringIncomeId);
    bool CheckIfIncomeIsRecurring(int incomeId);
    void AddRecurringIncomeAsIncome(int recurringIncomeId, int economyPlanId);
    RecurringIncome? GetRecurringIncomeFromIncome(int incomeId);
    IEnumerable<RecurringIncome> GetRecurringIncomesFromHouseholdGuid(string guid);
    IEnumerable<Income> GetIncomesFromEconomyPlan(int id);
}