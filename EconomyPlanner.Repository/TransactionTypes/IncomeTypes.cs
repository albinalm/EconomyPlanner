using System.Runtime.CompilerServices;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Languages;

namespace EconomyPlanner.Repository.TransactionTypes;

public static class IncomeType
{
    public static string Salary => CommonLoc.IncomeTypeSalary;
    public static string Refund => CommonLoc.IncomeTypeRefund;
    public static string Gift => CommonLoc.IncomeTypeGift;
    public static string Other => CommonLoc.TransactionTypeOther;

    public static IEnumerable<string> GetIncomeTypes()
    {
        var incomeTypes = typeof(IncomeType).GetProperties();
        foreach (var incomeType in incomeTypes)
        {
            var incomeTypeValue = incomeType.GetValue(incomeType, null)?.ToString();
            
            if (incomeTypeValue is not null)
                yield return incomeTypeValue;
        }
    }
}