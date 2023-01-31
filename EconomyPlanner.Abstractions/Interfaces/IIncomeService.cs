using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IIncomeService
{
    void CreateIncome(IncomeModel incomeModel);
    void UpdateIncome(Income income);
}