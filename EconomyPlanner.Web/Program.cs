using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EconomyPlanner.Web;
using EconomyPlanner.Web.Services;
using EconomyPlanner.Web.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
// builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("http://localhost:5179/") });
builder.Services.AddScoped<IHouseholdService, HouseholdService>();

await builder.Build().RunAsync();