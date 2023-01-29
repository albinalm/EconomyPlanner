using System.ComponentModel;
using EconomyPlanner.Repository.Languages;

namespace EconomyPlanner.Repository.Enums;

public enum IncomeType
{
    [Description(nameof(CommonLoc.IncomeTypeSalary))]
    Salary = 0,

    [Description(nameof(CommonLoc.IncomeTypeRefund))]
    Refund = 2,
    
    [Description(nameof(CommonLoc.IncomeTypeGift))]
    Gift = 3,
    
    [Description(nameof(CommonLoc.IncomeTypeOther))]
    Other = 4
}