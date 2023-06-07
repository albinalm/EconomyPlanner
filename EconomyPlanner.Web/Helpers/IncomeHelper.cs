using EconomyPlanner.Web.Models;

namespace EconomyPlanner.Web.Helpers;

public static class IncomeHelper
{
    public static decimal GetSumPerIncomeType(string incomeType, IEnumerable<IncomeModel> incomeModels)
    {
        return incomeModels.Where(e => e.IncomeType == incomeType).Select(e => e.Amount).Sum();
    }
}