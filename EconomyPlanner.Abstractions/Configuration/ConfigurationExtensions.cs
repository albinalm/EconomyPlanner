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
        
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("EconomyPlannerMigrationDummy")), ServiceLifetime.Singleton);
        
        services.AddSingleton<ITimeService, TimeService>();
        services.AddAutoMapper(typeof(MappingProfile));
        
        services.AddScoped<IEconomyPlanService, EconomyPlanService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IHouseholdService, HouseholdService>();
    }
}