using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Repository.Managers;

public class IncomeManager : IIncomeManager
{
    public Income Create(string name, decimal amount, IncomeType incomeType, bool recurring, decimal? recurringAmount)
    {
        if (recurring && recurringAmount is null)
            throw new InvalidOperationException("Recurring amount must have a value if recurring is set to true");
        
        return new Income(name, amount, incomeType, recurring, recurringAmount);
    }
}