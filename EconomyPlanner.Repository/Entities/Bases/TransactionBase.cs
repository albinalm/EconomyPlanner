using EconomyPlanner.Repository.Enums;

namespace EconomyPlanner.Repository.Entities.Bases;

public class TransactionBase : EntityBase
{
    protected decimal Amount { get; set; }

    protected TransactionBase(string name, decimal amount) : base(name)
    {
        Amount = amount;
    }
}