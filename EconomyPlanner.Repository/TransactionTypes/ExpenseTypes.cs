﻿namespace EconomyPlanner.Repository.TransactionTypes;

public static class ExpenseType
{
    public static string Shopping => "Shopping";
    public static string Household => "Hushåll";
    public static string Subscription => "Abonnemang";
    public static string Other => "Övrigt";
    
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