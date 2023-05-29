using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API.Services;

public interface IHouseholdService
{
    Household? GetHouseholdFromGuid(string guid);
    void CreateHousehold(string name);
}