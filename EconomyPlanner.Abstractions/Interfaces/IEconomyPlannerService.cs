using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IEconomyPlannerService
{
    void CreateEconomyPlan(EconomyPlanModel economyPlanModel);
    void AddExpense(EconomyPlan economyPlan, Expense expense);
    void DeleteExpense(EconomyPlan economyPlan, Expense expense);
    void AddIncome(EconomyPlan economyPlan, Income income);
    void DeleteIncome(EconomyPlan economyPlan, Income income);
    EconomyPlan? GetEconomyPlanByDate(DateTime startPeriod);
}