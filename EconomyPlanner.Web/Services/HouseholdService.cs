using System.Net.Http.Json;
using EconomyPlanner.Web.Services.Interfaces;

namespace EconomyPlanner.Web.Services;

public class HouseholdService : IHouseholdService
{
    private readonly HttpClient httpClient;

    public HouseholdService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> ValidateGuid(string guid)
    {
        return await httpClient.GetFromJsonAsync<bool>($"/api/Household/GetHouseholdFromGuid?guid={guid}");
    }
}