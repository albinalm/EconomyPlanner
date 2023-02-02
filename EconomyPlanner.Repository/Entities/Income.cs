using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class Income : TransactionBase
{
    public string IncomeType { get; set; }
    public RecurringIncome? RecurringIncome { get; set; }
    public Income(string name, decimal amount, string incomeType) : base(name, amount)
    {
        IncomeType = incomeType;
    }
    
    public static Income Create(string name, decimal amount, string incomeType, RecurringIncome? recurringIncome)
    {
        return new Income(name, amount, incomeType);
    }

    public void SetRecurringIncome(RecurringIncome? recurringIncome) => RecurringIncome = recurringIncome;
}