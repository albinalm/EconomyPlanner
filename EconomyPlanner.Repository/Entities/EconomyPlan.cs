using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class EconomyPlan : EntityBase
{
    public ICollection<Expense> Expenses { get; set; }
    public ICollection<Income> Incomes { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    
    public EconomyPlan(string name, string startDate, string endDate) : base(name)
    {
        StartDate = startDate;
        EndDate = endDate;
        Expenses = new HashSet<Expense>();
        Incomes = new HashSet<Income>();
    }
    
    public static EconomyPlan Create(string name)
    {
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;

        var startDate = $"{year}-" +
                        $"{month:00}-01";
        
        var endDate = $"{year}-" +
                      $"{month:00}-" +
                      $"{DateTime.DaysInMonth(year, month)}";
        
        return new EconomyPlan(name,
                               startDate,
                               endDate);

    }
}