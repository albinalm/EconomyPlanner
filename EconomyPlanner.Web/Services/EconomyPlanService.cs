using System.Net.Http.Json;
using EconomyPlanner.Web.Services.Interfaces;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;


namespace EconomyPlanner.Web.Services;

public class EconomyPlanService : IEconomyPlanService
{
    private readonly HttpClient _httpClient;
    private readonly IHouseholdService _householdService;

    public EconomyPlanService(HttpClient httpClient, IHouseholdService householdService)
    {
        _httpClient = httpClient;
        _householdService = householdService;
    }
    
    public async Task<IEnumerable<EconomyPlanModel>> GetEconomyPlans()
    {
        var hasLogin = await _householdService.HasSavedLogin();
        
        if (!hasLogin)
            throw new InvalidOperationException("ExpenseService > Login not valid");
        
        var householdModel = await _householdService.GetHouseholdModel();
        var economyPlanModels = await _httpClient.GetFromJsonAsync<IEnumerable<EconomyPlanModel>>($"http://localhost:5179/api/EconomyPlan/GetEconomyPlansFromHouseholdGuid?guid={householdModel.Guid}");

        if (economyPlanModels is null)
            throw new InvalidOperationException("EconomyPlanService > EconomyPlanModels is null");
        
        return economyPlanModels;
    }
}