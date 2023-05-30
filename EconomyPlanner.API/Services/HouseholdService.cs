using EconomyPlanner.API.Helpers;
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

    public void CreateHousehold(string name)
    {
        var currentDate = _timeService.GetNow();
        var futureMonthDate = currentDate.AddMonths(1);
        
        var household = Household.Create(name);
        
        _dbContext.Add(household);
        _dbContext.SaveChanges();

        _economyPlanService.SetupActiveEconomyPlans(household.Guid);
        //
        // _economyPlanService.CreateEconomyPlan(EconomyPlanHelper.GetEconomyPlanName(currentDate), household.Guid, currentDate);
        // _economyPlanService.CreateEconomyPlan(EconomyPlanHelper.GetEconomyPlanName(futureMonthDate), household.Guid, futureMonthDate);
    }
}