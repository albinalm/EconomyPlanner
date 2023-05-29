using EconomyPlanner.Web.Models;
using HouseholdModel = EconomyPlanner.Web.Models.HouseholdModel;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IHouseholdService
{
   Task<string?> GetGuid();
   Task<bool> AttemptLogin(string? guid);
   Task Logout();
   Task<bool> HasSavedLogin();
   Task<HouseholdModel> GetHouseholdModel();
   Task<IEnumerable<ExpenseModel>> GetRecurringExpenses();
   Task<IEnumerable<IncomeModel>> GetRecurringIncomes();
}