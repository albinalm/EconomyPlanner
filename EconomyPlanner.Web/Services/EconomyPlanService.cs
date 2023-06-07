using System.Net.Http.Json;
using EconomyPlanner.Web.Services.Interfaces;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;


namespace EconomyPlanner.Web.Services;

public class EconomyPlanService : IEconomyPlanService
{
    private readonly HttpClient _httpClient;
    private readonly IHouseholdService _householdService;
    private DateTime? _cachedDateTime;
    private readonly string? _domain;
    private readonly string? _port;
    private async Task<DateTime> ServerTime() => _cachedDateTime ?? await GetServerTime();
    public EconomyPlanService(HttpClient httpClient, IHouseholdService householdService, Options options)
    {
        _httpClient = httpClient;
        _householdService = householdService;
        _domain = options.ApiDomain;
        _port = options.ApiPort;
    }

    private async Task<DateTime> GetServerTime()
    {
        _cachedDateTime = await _httpClient.GetFromJsonAsync<DateTime>($"http://{_domain}:{_port}/api/Time/GetNow");
        return _cachedDateTime ?? DateTime.Now;
    }
    
    public async Task<IEnumerable<EconomyPlanModel>> GetEconomyPlans()
    {
        var hasLogin = await _householdService.HasSavedLogin();
        
        if (!hasLogin)
            throw new InvalidOperationException("ExpenseService > No login was detected");

        var economyPlanModels =
            (await _httpClient.GetFromJsonAsync<IEnumerable<EconomyPlanModel>>
                ($"http://{_domain}:{_port}/api/EconomyPlan/GetEconomyPlansFromHouseholdGuid?guid={await _householdService.GetGuid()}")
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
        
        await _httpClient.GetAsync($"http://{_domain}:{_port}/api/EconomyPlan/SetupActiveEconomyPlans?guid={await _householdService.GetGuid()}");
    }
    
    public async Task<IEnumerable<EconomyPlanModel>> TryGetOneYearEconomyPlanModels(string guid)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<EconomyPlanModel>>($"http://{_domain}:{_port}/api/EconomyPlan/TryGetOneYearEconomyPlans?guid={guid}") 
               ?? Enumerable.Empty<EconomyPlanModel>();
    }
}