using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Abstractions.Interfaces;

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
    void UpdateIncomeFromModel(IncomeModel incomeModel);
    void UpdateRecurringIncomeFromModel(IncomeModel incomeModel);
    IncomeModel? GetIncomeModel(int incomeId);
    IEnumerable<string> GetIncomeTypes();
    void DeleteIncome(int incomeId, bool deleteRecurring);
    void DeleteRecurringIncome(int recurringIncomeId);
    bool CheckIfIncomeIsRecurring(int incomeId);
    void AddRecurringIncomeAsIncome(int recurringIncomeId, int economyPlanId);
    IncomeModel GetRecurringIncomeFromIncome(int incomeId);
    IEnumerable<IncomeModel> GetAllIncomeModelsLinkedToRecurringIncome(int recurringIncomeId);
}