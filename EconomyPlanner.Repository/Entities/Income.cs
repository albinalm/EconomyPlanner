using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities;

public class Income
{
    public Income(string name, decimal amount, IncomeType incomeType,  bool recurring, decimal? recurringAmount)
    {
        Name = name;
        Amount = amount;
        IncomeType = incomeType;
        Recurring = recurring;
        RecurringAmount = recurringAmount;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public IncomeType IncomeType { get; set; }
    public bool Recurring { get; set; }
    public decimal? RecurringAmount { get; set; }
    public bool IsDeleted { get; set; }
}