namespace EconomyPlanner.Web.Models;

public class EconomyPlanModel
{
    public int Id { get; set; }
    public string Name { get; set; } 
    
    public string EndDate { get; set; }
    public bool IsActive { get; set; }
}