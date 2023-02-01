using EconomyPlanner.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyPlanner.Repository.Configuration;

public class DatabaseContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<RecurringExpense> RecurringExpenses { get; set; }
    public DbSet<RecurringIncome> RecurringIncomes { get; set; }
    public DbSet<EconomyPlan> EconomyPlans { get; set; }
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public EconomyPlan? GetEconomyPlanFromId(int economyPlanId)
    {
        return EconomyPlans.Include(ep => ep.Expenses)
                           .Include(ep => ep.Incomes)
                           .FirstOrDefault(ep => ep.Id == economyPlanId);
    }

    public Expense? GetExpenseFromId(int expenseId) => Expenses.Find(expenseId);
    public Income? GetIncomeFromId(int incomeId) => Incomes.Find(incomeId);
    public RecurringExpense? GetRecurringExpenseFromId(int recurringExpenseId) => RecurringExpenses.Find(recurringExpenseId);
    public RecurringIncome? GetRecurringIncomeFromId(int recurringIncomeId) => RecurringIncomes.Find(recurringIncomeId);
    
}