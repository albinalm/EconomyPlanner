namespace EconomyPlanner.Repository.Entities.Bases;

public class TransactionBase : EntityBase
{
    public decimal Amount { get; set; }

    public TransactionBase(string name, decimal amount) : base(name)
    {
        Amount = amount;
    }
}