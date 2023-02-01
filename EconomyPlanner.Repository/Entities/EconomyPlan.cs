namespace EconomyPlanner.Repository.Entities;

public class EconomyPlan
{
#pragma warning disable CS8618 //Disabled due to Entity framework cannot use navigation properties in constructor. Parameters are enforced in manager
    public EconomyPlan(string name, string startDate, string endDate)
#pragma warning restore CS8618
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Expenses = new HashSet<Expense>();
        Incomes = new HashSet<Income>();
    }

    public int Id { get; set; }
    public string Name { get; set; } 
    public ICollection<Expense> Expenses { get; set; }
    public ICollection<Income> Incomes { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
  //  public string UserGuid { get; set; }
}