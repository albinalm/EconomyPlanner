using EconomyPlanner.Repository.Entities.Bases;
using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class Income : TransactionBase
{
    public IncomeType IncomeType { get; set; }
    public RecurringIncome RecurringIncome { get; set; }
    public Income(string name, decimal amount, IncomeType incomeType, RecurringIncome recurringIncome) : base(name, amount)
    {
        IncomeType = incomeType;
        RecurringIncome = recurringIncome;
    }
}