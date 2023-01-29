using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EconomyPlanner.Repository.Languages;

namespace EconomyPlanner.Repository.Enums;

public enum ExpenseType
{
    [Description(nameof(CommonLoc.ExpenseTypeShopping))]
    Shopping = 0,
    
    [Description(nameof(CommonLoc.ExpenseTypeHousehold))]
    Household = 1,
    
    [Description(nameof(CommonLoc.ExpenseTypeSubscription))]
    Subscription = 2,
    
    [Description(nameof(CommonLoc.ExpenseTypeOther))]
    Other = 3
}