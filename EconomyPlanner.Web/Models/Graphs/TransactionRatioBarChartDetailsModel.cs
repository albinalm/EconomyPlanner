namespace EconomyPlanner.Web.Models.Graphs;

public class TransactionRatioBarChartDetailsModel
{
    public string Month { get; set; } = "";
    public decimal Amount { get; set; }
    public string AmountReadable { get; set; } = "";
    public decimal Savings { get; set; }
    public string SavingsReadable { get; set; } = "";
}