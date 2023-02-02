using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IIncomeService
{
    void CreateIncome(int economyPlanId, string name, decimal amount, string incomeType, bool isRecurring);
    void UpdateIncomeFromModel(IncomeModel incomeModel);
    IncomeModel? GetIncomeModel(int incomeId);
}