﻿using System.Net;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Web.Services.Interfaces;

namespace EconomyPlanner.Web.Services;

public class HouseholdService : IHouseholdService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public HouseholdService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        this._httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<bool> AttemptLogin(string guid)
    {
        var loginSuccessful = await _httpClient.GetFromJsonAsync<HouseholdModel>($"http://localhost:5179/api/Household/GetHouseholdFromGuid?guid={guid}") != null;
        
        if (loginSuccessful)
            await _localStorageService.SetItemAsync("EconomyPlanner.UserGuid", guid);
        
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

    public async Task<string?> GetSavedLogin()
    {
        return await HasSavedLogin() ? await _localStorageService.GetItemAsync<string>("EconomyPlanner.UserGuid") : null;
    }

    public async Task<HouseholdModel> GetHouseholdModel()
    {
        if (!await HasSavedLogin())
            throw new InvalidOperationException("");

        var householdModel = await _httpClient.GetFromJsonAsync<HouseholdModel>($"http://localhost:5179/api/Household/GetHouseholdFromGuid?guid={await GetSavedLogin()}");
        
        if (householdModel is null)
            throw new InvalidOperationException("Could not find household via login");

        return householdModel;
    }
}