namespace EconomyPlanner.Web.Models.Graphs;

public class ExpenseAverageChartDetailsModel
{
    public string? Type { get; set; }
    public decimal CurrentAmount { get; set; }
    public string? CurrentAmountReadable { get; set; }
    public decimal AverageAmount { get; set; }
    public string? AverageAmountReadable { get; set; }
}