using System.Net.Http.Json;
using Blazored.LocalStorage;
using EconomyPlanner.Web.Models;
using EconomyPlanner.Web.Services.Interfaces;
using HouseholdModel = EconomyPlanner.Web.Models.HouseholdModel;

namespace EconomyPlanner.Web.Services;

public class HouseholdService : IHouseholdService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public HouseholdService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }
    private string? SessionGuidCache { get; set; }
    
    public async Task<string?> GetGuid() => SessionGuidCache ?? await GetSavedLogin();

    public async Task<bool> AttemptLogin(string? guid)
    {
        if (guid is null)
        {
            return false;
        }

        var loginSuccessful = false;
        
        try
        {
            loginSuccessful = await _httpClient.GetFromJsonAsync<HouseholdModel>($"http://192.168.1.103:6320/api/Household/GetHouseholdFromGuid?guid={guid}") != null;   
        }
        catch{}
        
        if (!loginSuccessful) return loginSuccessful;
        
        await _localStorageService.SetItemAsync("EconomyPlanner.UserGuid", guid);
        SessionGuidCache = guid;

        return loginSuccessful;
    }

    public async Task Logout()
    {
        await _localStorageService.RemoveItemAsync("EconomyPlanner.UserGuid");
    }

    public async Task<bool> HasSavedLogin()
    {
        return await _localStorageService.GetItemAsync<string>("EconomyPlanner.UserGuid") != null;
    }

    public async Task<HouseholdModel> GetHouseholdModel()
    {
        if (!await HasSavedLogin())
            throw new InvalidOperationException("No valid login");

        var householdModel = await _httpClient.GetFromJsonAsync<HouseholdModel>($"http://192.168.1.103:6320/api/Household/GetHouseholdFromGuid?guid={await GetGuid()}");
        
        if (householdModel is null)
            throw new InvalidOperationException("Could not find household via login");

        return householdModel;
    }

    public async Task<IEnumerable<ExpenseModel>> GetRecurringExpenses()
    {
        if (!await HasSavedLogin())
            throw new InvalidOperationException("No valid login");
        
        var expenseModels = await _httpClient.GetFromJsonAsync<IEnumerable<ExpenseModel>>($"http://192.168.1.103:6320/api/Expense/GetRecurringExpensesFromHouseholdGuid?guid={await GetGuid()}");
        
        if (expenseModels is null)
            throw new InvalidOperationException("Could not find household via login");

        return expenseModels;
    }
    
    public async Task<IEnumerable<IncomeModel>> GetRecurringIncomes()
    {
        if (!await HasSavedLogin())
            throw new InvalidOperationException("No valid login");
        
        var incomeModels = await _httpClient.GetFromJsonAsync<IEnumerable<IncomeModel>>($"http://192.168.1.103:6320/api/Income/GetRecurringIncomesFromHouseholdGuid?guid={await GetGuid()}");
        
        if (incomeModels is null)
            throw new InvalidOperationException("Could not find household via login");

        return incomeModels;
    }
    
    private async Task<string?> GetSavedLogin()
    {
        return await HasSavedLogin() ? await _localStorageService.GetItemAsync<string>("EconomyPlanner.UserGuid") : null;
    }

    public async Task<HouseholdModel> CreateHousehold()
    {
        var householdModel = await _httpClient.GetFromJsonAsync<HouseholdModel>($"http://192.168.1.103:6320/api/Household/CreateHousehold?name={Guid.NewGuid().ToString()}");
        
        if (householdModel is null)
            throw new ApplicationException("CreateHousehold failed!");
        
        return householdModel;
    }
}