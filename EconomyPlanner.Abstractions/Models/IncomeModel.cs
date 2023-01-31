using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Abstractions.Models;

public class IncomeModel
{
    public IncomeModel(string name, decimal amount, IncomeType incomeType,  bool recurring, decimal? recurringAmount)
    {
        Name = name;
        Amount = amount;
        IncomeType = incomeType;
        Recurring = recurring;
        RecurringAmount = recurringAmount;
    }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public IncomeType IncomeType { get; set; }
    public bool Recurring { get; set; }
    public decimal? RecurringAmount { get; set; }
    public bool IsDeleted { get; set; }
}