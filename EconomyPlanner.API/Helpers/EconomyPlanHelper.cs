using EconomyPlanner.API.Extensions;

namespace EconomyPlanner.API.Helpers;

public static class EconomyPlanHelper
{
    public static string GetEconomyPlanName(DateTime now)
    {
        return $"{now.Year} - {now.ToString("MMMM").FirstCharToUpper()}";
    }
}