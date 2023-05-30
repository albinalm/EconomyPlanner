using EconomyPlanner.API.Interfaces;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API.Services;

public class HouseholdService : IHouseholdService
{
    private readonly DatabaseContext _dbContext;
    private readonly IEconomyPlanService _economyPlanService;
    private readonly ITimeService _timeService;

    public HouseholdService(DatabaseContext dbContext, IEconomyPlanService economyPlanService, ITimeService timeService)
    {
        _dbContext = dbContext;
        _economyPlanService = economyPlanService;
        _timeService = timeService;
    }

    public Household? GetHouseholdFromGuid(string guid)
    {
        return _dbContext.GetHouseholdFromGuid(guid);
    }

    public Household CreateHousehold(string name)
    {
        var household = Household.Create(name);
        
        _dbContext.Add(household);
        _dbContext.SaveChanges();

        _economyPlanService.SetupActiveEconomyPlans(household.Guid);
        
        return household;
    }
}