using System.Net.Http.Json;
using EconomyPlanner.Web.Models;
using EconomyPlanner.Web.Services.Interfaces;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;
using ExpenseModel = EconomyPlanner.Web.Models.ExpenseModel;

namespace EconomyPlanner.Web.Services;

public class ExpenseService : IExpenseService
{
    private readonly HttpClient _httpClient;
    private readonly string? _domain;
    private readonly string? _port;
    private IEnumerable<string>? _cachedExpenseTypes;
    private IEnumerable<ExpenseModel>? _cachedExpenseModels;
    private IEnumerable<ExpenseModel>? _cachedOneYearExpenseModels;
    private EconomyPlanModel? _selectedEconomyPlan;
    public async Task<IEnumerable<string>> ExpenseTypes() => _cachedExpenseTypes ?? await GetExpenseTypes();
    public async Task<IEnumerable<ExpenseModel>> ExpenseModels(EconomyPlanModel? economyPlanModel)
    {
        Console.WriteLine($"economyPlanModel is: {economyPlanModel?.Name} {economyPlanModel?.Id} Selected is: {_selectedEconomyPlan?.Name} and cached expense model count is: {_cachedExpenseModels?.Count()}");
        
        if (economyPlanModel is null)
            return Enumerable.Empty<ExpenseModel>();
        
        if (_selectedEconomyPlan is null)
            return await GetExpensesFromEconomyPlan(economyPlanModel);
        
        if (economyPlanModel == _selectedEconomyPlan && _cachedExpenseModels is not null)
            return _cachedExpenseModels;

        var expenses = await GetExpensesFromEconomyPlan(economyPlanModel);
     
        _cachedExpenseModels = expenses;
        _selectedEconomyPlan = economyPlanModel;
        
        return await GetExpensesFromEconomyPlan(economyPlanModel);
    }

    public async Task<IEnumerable<ExpenseModel>> OneYearExpenseModels(string guid) 
        => _cachedOneYearExpenseModels ?? await GetAllExpenseModelsFromLastYearEconomyPlans(guid);
    public ExpenseService(HttpClient httpClient, Options options)
    {
        _httpClient = httpClient;
        _domain = options.ApiDomain;
        _port = options.ApiPort;
    }

    public async Task<IEnumerable<ExpenseModel>> GetExpensesFromEconomyPlan(EconomyPlanModel economyPlanModel)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ExpenseModel>>($"http://{_domain}:{_port}/api/Expense/GetExpensesFromEconomyPlan?id={economyPlanModel.Id}")
               ?? Enumerable.Empty<ExpenseModel>();
    }

    public async Task UpdateExpense(ExpenseModel expenseModel)
    {
        await _httpClient.PostAsJsonAsync($"http://{_domain}:{_port}/api/Expense/UpdateExpense", expenseModel);
    }

    private async Task<IEnumerable<string>> GetExpenseTypes()
    {
        var expenseTypes = await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"http://{_domain}:{_port}/api/Expense/GetExpenseTypes") ?? Enumerable.Empty<string>();
        
        _cachedExpenseTypes = expenseTypes;
        
        return _cachedExpenseTypes;
    }

    public async Task DeleteExpense(ExpenseModel expenseModel, bool deleteRecurring)
    { 
        await _httpClient.GetAsync($"http://{_domain}:{_port}/api/Expense/DeleteExpense?expenseId={expenseModel.Id}&deleteRecurring={deleteRecurring}");
    }

    public async Task<bool> CheckIfExpenseIsRecurring(ExpenseModel expenseModel)
    {
        return await _httpClient.GetFromJsonAsync<bool>($"http://{_domain}:{_port}/api/Expense/CheckIfExpenseIsRecurring?expenseId={expenseModel.Id}");
    }

    public async Task AddExpense(CreateExpenseModel createExpenseModel)
    {
        await _httpClient.PostAsJsonAsync($"http://{_domain}:{_port}/api/Expense/CreateExpense", createExpenseModel);
    }

    public async Task AddRecurringExpense(CreateExpenseModel createExpenseModel)
    {
        await _httpClient.PostAsJsonAsync($"http://{_domain}:{_port}/api/Expense/CreateRecurringExpense", createExpenseModel);
    }
    
    public async Task UpdateRecurringExpense(ExpenseModel expenseModel)
    {
        await _httpClient.PostAsJsonAsync($"http://{_domain}:{_port}/api/Expense/UpdateRecurringExpense", expenseModel);
    }
    
    public async Task DeleteRecurringExpense(ExpenseModel expenseModel)
    { 
        await _httpClient.GetAsync($"http://{_domain}:{_port}/api/Expense/DeleteRecurringExpense?recurringExpenseId={expenseModel.Id}");
    }
    
    public async Task AddRecurringExpenseAsExpense(ExpenseModel expenseModel, EconomyPlanModel economyPlanModel)
    {
        await _httpClient.GetAsync($"http://{_domain}:{_port}/api/Expense/AddRecurringExpenseAsExpense?recurringExpenseId={expenseModel.Id}&economyPlanId={economyPlanModel.Id}");
    }

    public async Task<IEnumerable<ExpenseModel>> GetAllExpensesLinkedToRecurringExpense(ExpenseModel expenseModel)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ExpenseModel>>($"http://{_domain}:{_port}/api/Expense/GetAllExpensesLinkedToRecurringExpense?recurringExpenseId={expenseModel.Id}") 
               ?? Enumerable.Empty<ExpenseModel>();
    }
    
    public async Task<ExpenseModel?> GetRecurringExpenseFromExpense(ExpenseModel expenseModel)
    {
        return await _httpClient.GetFromJsonAsync<ExpenseModel?>($"http://{_domain}:{_port}/api/Expense/GetRecurringExpenseFromExpense?expenseId={expenseModel.Id}");
    }

    private async Task<IEnumerable<ExpenseModel>> GetAllExpenseModelsFromLastYearEconomyPlans(string guid)
    {
        var expenseModels = await _httpClient.GetFromJsonAsync<IEnumerable<ExpenseModel>>($"http://{_domain}:{_port}/api/Expense/GetAllExpensesFromLastYearEconomyPlans?guid={guid}") 
                            ?? Enumerable.Empty<ExpenseModel>();

        _cachedOneYearExpenseModels = expenseModels;
        return _cachedOneYearExpenseModels;
    }

    public void ClearCache()
    {
        _cachedExpenseModels = null;
        _cachedOneYearExpenseModels = null;
    }
}