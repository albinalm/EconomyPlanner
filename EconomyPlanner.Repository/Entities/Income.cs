using EconomyPlanner.Repository.Entities.Bases;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class Income : TransactionBase
{
    public IncomeType IncomeType { get; set; }
    public RecurringIncome? RecurringIncome { get; set; }
    public Income(string name, decimal amount, IncomeType incomeType) : base(name, amount)
    {
        IncomeType = incomeType;
    }
    
    public static Income Create(string name, decimal amount, IncomeType incomeType, RecurringIncome? recurringIncome)
    {
        return new Income(name, amount, incomeType);
    }

    public void SetRecurringIncome(RecurringIncome? recurringIncome) => RecurringIncome = recurringIncome;
}