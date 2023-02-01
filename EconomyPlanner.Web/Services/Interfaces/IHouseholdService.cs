namespace EconomyPlanner.Web.Services.Interfaces;

public interface IHouseholdService
{
    Task<bool> ValidateGuid(string guid);
}