using EconomyPlanner.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyPlanner.Repository.Configuration;

public class DatabaseContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<EconomyPlan> EconomyPlans { get; set; }
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
}