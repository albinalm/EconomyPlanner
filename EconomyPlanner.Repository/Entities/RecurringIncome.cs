using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class RecurringIncome : TransactionBase
{
    public string IncomeType { get; set; }
    
    public RecurringIncome(string name, decimal amount, string incomeType) : base(name, amount)
    {
        IncomeType = incomeType;
    }
    
    public static RecurringIncome Create(string name, decimal amount, string incomeType)
    {
        return new RecurringIncome(name, amount, incomeType);
    }
}