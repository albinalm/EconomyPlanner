using EconomyPlanner.Web.Models;

namespace EconomyPlanner.Web.Helpers;

public static class ExpenseHelper
{
    public static decimal GetSumPerExpenseType(string? expenseType, IEnumerable<ExpenseModel> expenseModels)
    {
        return expenseType is null ? 0 : expenseModels.Where(e => e.ExpenseType == expenseType).Select(e => e.Amount).Sum();
    }
}