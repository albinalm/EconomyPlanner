using EconomyPlanner.Repository.Entities.Bases;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class RecurringIncome : TransactionBase
{
    public IncomeType IncomeType { get; set; }
    
    public RecurringIncome(string name, decimal amount, IncomeType incomeType) : base(name, amount)
    {
        IncomeType = incomeType;
    }
    
    public static RecurringIncome Create(string name, decimal amount, IncomeType incomeType)
    {
        return new RecurringIncome(name, amount, incomeType);
    }
}