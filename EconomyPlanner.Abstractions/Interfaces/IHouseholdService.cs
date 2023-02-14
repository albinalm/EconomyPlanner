using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IHouseholdService
{
    HouseholdModel? GetHouseholdByGuid(string guid);
    void CreateHousehold(string name);
    IEnumerable<ExpenseModel> GetRecurringExpenses(string guid);
}