using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API.Services;

public interface IHouseholdService
{
    Household? GetHouseholdFromGuid(string guid);
    Household CreateHousehold(string name);
}