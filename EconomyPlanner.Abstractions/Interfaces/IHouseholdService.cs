using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IHouseholdService
{
    HouseholdModel? GetHouseholdByGuid(string guid);
    void CreateHousehold(string name);
}