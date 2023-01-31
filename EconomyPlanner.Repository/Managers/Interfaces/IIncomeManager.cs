using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Managers.Interfaces;

public interface IIncomeManager
{
    Income Create(string name, decimal amount, IncomeType incomeType, bool recurring, decimal? recurringAmount);
}