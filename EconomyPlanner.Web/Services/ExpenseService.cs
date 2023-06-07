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
    public ExpenseService(HttpClient httpClient, Options options)
    {
        _httpClient = httpClient;
        _domain = options.ApiDomain;
        _port = options.ApiPort;
    }

    public async Task<IEnumerable<ExpenseModel>> GetExpenses(EconomyPlanModel economyPlanModel)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ExpenseModel>>($"http://{_domain}:{_port}/api/Expense/GetExpensesFromEconomyPlan?id={economyPlanModel.Id}")
               ?? Enumerable.Empty<ExpenseModel>();
    }
    
    public async Task UpdateExpense(ExpenseModel expenseModel)
    {
        await _httpClient.PostAsJsonAsync($"http://{_domain}:{_port}/api/Expense/UpdateExpense", expenseModel);
    }

    public async Task<IEnumerable<string>> GetExpenseTypes()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"http://{_domain}:{_port}/api/Expense/GetExpenseTypes") ?? Enumerable.Empty<string>();
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

    public async Task<IEnumerable<ExpenseModel>> GetAllExpenseModelsFromLastYearEconomyPlans(string guid)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ExpenseModel>>($"http://{_domain}:{_port}/api/Expense/GetAllExpensesFromLastYearEconomyPlans?guid={guid}") 
               ?? Enumerable.Empty<ExpenseModel>();
    }
}