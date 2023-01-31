using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Models;

public class EconomyPlanModel
{
    public EconomyPlanModel(string name, ICollection<Expense> expenses, ICollection<Income> incomes, string startDate, string endDate)
    {
        
        Name = name;
        Expenses = expenses;
        Incomes = incomes;
        StartDate = startDate;
        EndDate = endDate;
    }
    public string Name { get; set; } 
    public ICollection<Expense> Expenses { get; set; }
    public ICollection<Income> Incomes { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}