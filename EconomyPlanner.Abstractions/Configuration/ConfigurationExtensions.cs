using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Services;
using EconomyPlanner.Repository.Configuration;
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

        services.AddTransient<IEconomyPlannerService, EconomyPlanService>();
        services.AddTransient<IExpenseService, ExpenseService>();
        services.AddTransient<IIncomeService, IncomeService>();
        
        services.AddAutoMapper(typeof(MappingProfile));
    }
}