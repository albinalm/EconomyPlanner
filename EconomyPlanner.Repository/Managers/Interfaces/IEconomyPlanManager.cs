using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Repository.Managers.Interfaces;

public interface IEconomyPlanManager
{
    EconomyPlan Create(string name, ICollection<Expense>? expenses, ICollection<Income>? incomes);
}