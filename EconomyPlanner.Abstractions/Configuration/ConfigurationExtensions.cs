using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Abstractions.Services;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Managers;
using EconomyPlanner.Repository.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyPlanner.Abstractions.Configuration;

public static class ConfigurationExtensions
{
    public static void RegisterRepository(this IServiceCollection services, IConfigurationRoot config)
    {
        var connectionString = config.GetConnectionString("Domain");
            
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("A connection string needs to be provided");
        
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("EconomyPlannerMigrationDummy")));
       
        services.AddTransient<IIncomeManager, IncomeManager>();
        services.AddTransient<IExpenseManager, ExpenseManager>();
        services.AddTransient<IEconomyPlanManager, EconomyPlanManager>();
        
        services.AddTransient<IEconomyPlannerService, EconomyPlanService>();
        services.AddTransient<IExpenseService, ExpenseService>();
        services.AddTransient<IIncomeService, IncomeService>();
        
        services.AddAutoMapper(typeof(MappingProfile));
    }
}