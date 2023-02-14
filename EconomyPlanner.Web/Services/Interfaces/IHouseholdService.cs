using EconomyPlanner.Web.Models;
using HouseholdModel = EconomyPlanner.Web.Models.HouseholdModel;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IHouseholdService
{
   Task<bool> AttemptLogin(string guid);
   Task Logout();
   Task<bool> HasSavedLogin();
   Task<string?> GetSavedLogin();
   Task<HouseholdModel> GetHouseholdModel();
   Task<IEnumerable<ExpenseModel>> GetRecurringExpenses();
}