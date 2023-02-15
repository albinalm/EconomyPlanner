using System.Net.Http.Json;
using System.Runtime.InteropServices;
using EconomyPlanner.Web.Models;
using EconomyPlanner.Web.Services.Interfaces;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;
using ExpenseModel = EconomyPlanner.Web.Models.ExpenseModel;

namespace EconomyPlanner.Web.Services;

public class ExpenseService : IExpenseService
{
    private readonly HttpClient _httpClient;
    private readonly IHouseholdService _householdService;

    public ExpenseService(HttpClient httpClient, IHouseholdService householdService)
    {
        _httpClient = httpClient;
        _householdService = householdService;
    }

    public IEnumerable<ExpenseModel> GetExpenses(EconomyPlanModel economyPlanModel)
    {
        return economyPlanModel.ExpenseModels;
    }

    public async Task UpdateExpense(ExpenseModel expenseModel)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5179/api/Expense/UpdateExpense", expenseModel);
    }

    public async Task<IEnumerable<string>> GetExpenseTypes()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<string>>("http://localhost:5179/api/Expense/GetExpenseTypes") ?? Enumerable.Empty<string>();
    }

    public async Task DeleteExpense(ExpenseModel expenseModel, bool deleteRecurring)
    { 
        await _httpClient.GetAsync($"http://localhost:5179/api/Expense/DeleteExpense?expenseId={expenseModel.Id}&deleteRecurring={deleteRecurring}");
    }

    public async Task<bool> CheckIfExpenseIsRecurring(ExpenseModel expenseModel)
    {
        return await _httpClient.GetFromJsonAsync<bool>($"http://localhost:5179/api/Expense/CheckIfExpenseIsRecurring?expenseId={expenseModel.Id}");
    }

    public async Task AddExpense(CreateExpenseModel createExpenseModel)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5179/api/Expense/CreateExpense", createExpenseModel);
    }
}