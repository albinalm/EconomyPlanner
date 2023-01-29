using System.Collections;

namespace EconomyPlanner.Repository.Entities;

public class EconomyPlan
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public virtual IEnumerable<Expense> Expenses { get; set; }
    public virtual IEnumerable<Income> Incomes { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}