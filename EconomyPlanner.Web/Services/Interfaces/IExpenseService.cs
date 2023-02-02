using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IExpenseService
{
    IEnumerable<ExpenseModel> GetExpenses(EconomyPlanModel economyPlanModel);
}