using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class Household : EntityBase
{
    public string Guid { get; set; }
    public ICollection<EconomyPlan> EconomyPlans { get; set; }
    public ICollection<RecurringExpense> RecurringExpenses { get; set; }
    public ICollection<RecurringIncome> RecurringIncomes { get; set; }
    
    public Household(string name) : base(name)
    {
        Guid = new Random().Next().ToString("x");
        EconomyPlans = new HashSet<EconomyPlan>();
        RecurringExpenses = new HashSet<RecurringExpense>();
        RecurringIncomes = new HashSet<RecurringIncome>();
    }
    
    public static Household Create(string name)
    {
        return new Household(name);
    }
}