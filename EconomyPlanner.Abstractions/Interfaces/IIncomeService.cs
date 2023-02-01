using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IIncomeService
{
    void CreateIncome(int economyPlanId, string name, decimal amount, int incomeTypeId, bool isRecurring);
    void UpdateIncomeFromModel(IncomeModel incomeModel);
    IncomeModel? GetIncomeModel(int incomeId);
}