using System.Net.Http.Json;
using EconomyPlanner.Web.Services.Interfaces;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;


namespace EconomyPlanner.Web.Services;

public class EconomyPlanService : IEconomyPlanService
{
    private readonly HttpClient _httpClient;
    private readonly IHouseholdService _householdService;
    private DateTime? _cachedDateTime;
    private async Task<DateTime> ServerTime() => _cachedDateTime ?? await GetServerTime();
    public EconomyPlanService(HttpClient httpClient, IHouseholdService householdService)
    {
        _httpClient = httpClient;
        _householdService = householdService;
    }

    private async Task<DateTime> GetServerTime()
    {
        _cachedDateTime = await _httpClient.GetFromJsonAsync<DateTime>("http://localhost:5179/api/Time/GetNow");
        return _cachedDateTime ?? DateTime.Now;
    }
    
    public async Task<IEnumerable<EconomyPlanModel>> GetEconomyPlans()
    {
        var hasLogin = await _householdService.HasSavedLogin();
        
        if (!hasLogin)
            throw new InvalidOperationException("ExpenseService > No login was detected");

        var economyPlanModels =
            (await _httpClient.GetFromJsonAsync<IEnumerable<EconomyPlanModel>>
                ($"http://localhost:5179/api/EconomyPlan/GetEconomyPlansFromHouseholdGuid?guid={await _householdService.GetGuid()}")
            ?? Enumerable.Empty<EconomyPlanModel>())
            .ToList();
        
        var currentDate = await ServerTime();
        
        economyPlanModels.ForEach(ep =>
        {
            ep.IsActive = currentDate <= DateTime.Parse(ep.EndDate);
        });
        
        return economyPlanModels;
    }

    public async Task SetupActiveEconomyPlans()
    {
        var hasLogin = await _householdService.HasSavedLogin();
        
        if (!hasLogin)
            throw new InvalidOperationException("ExpenseService > No login was detected");
        
        await _httpClient.GetAsync($"http://localhost:5179/api/EconomyPlan/SetupActiveEconomyPlans?guid={await _householdService.GetGuid()}");
    }
}