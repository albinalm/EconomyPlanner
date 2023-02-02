namespace EconomyPlanner.Web.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseType { get; set; }
        public bool Recurring { get; set; }
        public decimal? RecurringAmount { get; set; }
        public bool IsDeleted { get; set; }
    }
}