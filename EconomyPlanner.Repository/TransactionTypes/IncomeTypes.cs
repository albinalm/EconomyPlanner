namespace EconomyPlanner.Repository.TransactionTypes;

public static class IncomeType
{
    public static string Salary => "Lön";
    public static string Refund => "Återbetalning";
    public static string Gift => "Gåva";
    public static string Other => "Övrigt";

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