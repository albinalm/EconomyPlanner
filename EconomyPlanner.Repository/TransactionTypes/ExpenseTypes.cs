using EconomyPlanner.Repository.Languages;

namespace EconomyPlanner.Repository.TransactionTypes;

public static class ExpenseType
{
    public static string Shopping => CommonLoc.ExpenseTypeShopping;
    public static string Household => CommonLoc.ExpenseTypeHousehold;
    public static string Subscription => CommonLoc.ExpenseTypeSubscription;
    public static string Other => CommonLoc.TransactionTypeOther;
    
    public static IEnumerable<string> GetExpenseTypes()
    {
        var expenseTypes = typeof(ExpenseType).GetProperties();
        foreach (var expenseType in expenseTypes)
        {
            var expenseTypeValue = expenseType.GetValue(expenseType, null)?.ToString();
            
            if (expenseTypeValue is not null)
                yield return expenseTypeValue;
        }
    }
}