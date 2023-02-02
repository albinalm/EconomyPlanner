using EconomyPlanner.Repository.Languages;

namespace EconomyPlanner.Repository.TransactionTypes;

public static class TransactionType
{
    public static string Shopping => CommonLoc.ExpenseTypeShopping;
    public static string Household => CommonLoc.ExpenseTypeHousehold;
    public static string Subscription => CommonLoc.ExpenseTypeSubscription;
    
    public static string Salary => CommonLoc.IncomeTypeSalary;
    public static string Refund => CommonLoc.IncomeTypeRefund;
    public static string Gift => CommonLoc.IncomeTypeGift;
    
    public static string Other => CommonLoc.TransactionTypeOther;
}