using System.Net.Http.Json;
using EconomyPlanner.Web.Models;
using EconomyPlanner.Web.Services.Interfaces;

namespace EconomyPlanner.Web.Services;

public class IncomeService : IIncomeService
{
    private readonly HttpClient _httpClient;
    private readonly IHouseholdService _householdService;

    public IncomeService(HttpClient httpClient, IHouseholdService householdService)
    {
        _httpClient = httpClient;
        _householdService = householdService;
    }

    public IEnumerable<IncomeModel> GetIncomes(EconomyPlanModel economyPlanModel)
    {
        return economyPlanModel.IncomeModels;
    }

    public async Task UpdateIncome(IncomeModel incomeModel)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5179/api/Income/UpdateIncome", incomeModel);
    }

    public async Task<IEnumerable<string>> GetIncomeTypes()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<string>>("http://localhost:5179/api/Income/GetIncomeTypes") ?? Enumerable.Empty<string>();
    }

    public async Task DeleteIncome(IncomeModel incomeModel, bool deleteRecurring)
    { 
        await _httpClient.GetAsync($"http://localhost:5179/api/Income/DeleteIncome?incomeId={incomeModel.Id}&deleteRecurring={deleteRecurring}");
    }

    public async Task<bool> CheckIfIncomeIsRecurring(IncomeModel incomeModel)
    {
        return await _httpClient.GetFromJsonAsync<bool>($"http://localhost:5179/api/Income/CheckIfIncomeIsRecurring?incomeId={incomeModel.Id}");
    }

    public async Task AddIncome(CreateIncomeModel createIncomeModel)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5179/api/Income/CreateIncome", createIncomeModel);
    }

    public async Task AddRecurringIncome(CreateIncomeModel createIncomeModel)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5179/api/Income/CreateRecurringIncome", createIncomeModel);
    }
    
    public async Task UpdateRecurringIncome(IncomeModel incomeModel)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5179/api/Income/UpdateRecurringIncome", incomeModel);
    }
    
    public async Task DeleteRecurringIncome(IncomeModel incomeModel)
    { 
        await _httpClient.GetAsync($"http://localhost:5179/api/Income/DeleteRecurringIncome?recurringIncomeId={incomeModel.Id}");
    }
    
    public async Task AddRecurringIncomeAsIncome(IncomeModel incomeModel, EconomyPlanModel economyPlanModel)
    {
        await _httpClient.GetAsync($"http://localhost:5179/api/Income/AddRecurringIncomeAsIncome?recurringIncomeId={incomeModel.Id}&economyPlanId={economyPlanModel.Id}");
    }

    public async Task<IEnumerable<IncomeModel>> GetAllIncomesLinkedToRecurringIncome(IncomeModel incomeModel)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<IncomeModel>>($"http://localhost:5179/api/Income/GetAllIncomesLinkedToRecurringIncome?recurringIncomeId={incomeModel.Id}") ?? Enumerable.Empty<IncomeModel>();
    }
    
    public async Task<IncomeModel?> GetRecurringIncomeFromIncome(IncomeModel incomeModel)
    {
        return await _httpClient.GetFromJsonAsync<IncomeModel?>($"http://localhost:5179/api/Income/GetRecurringIncomeFromIncome?incomeId={incomeModel.Id}");
    }
}