using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Models;

public class EconomyPlanModel
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public ICollection<ExpenseModel> ExpenseModels { get; set; }
    public ICollection<IncomeModel> IncomeModels { get; set; }
}