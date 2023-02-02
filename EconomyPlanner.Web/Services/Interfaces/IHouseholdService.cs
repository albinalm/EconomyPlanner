using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IHouseholdService
{
   Task<bool> AttemptLogin(string guid);
   Task Logout();
   Task<bool> HasSavedLogin();
   Task<string?> GetSavedLogin();
   Task<HouseholdModel> GetHouseholdModel();
}