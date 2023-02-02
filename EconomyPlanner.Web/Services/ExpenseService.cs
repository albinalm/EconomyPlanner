using System.Net.Http.Json;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Web.Services.Interfaces;

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
}