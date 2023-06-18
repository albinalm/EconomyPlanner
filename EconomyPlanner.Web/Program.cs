
using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EconomyPlanner.Web;
using EconomyPlanner.Web.Services;
using EconomyPlanner.Web.Services.Interfaces;
using Havit.Blazor.Components.Web;            // <------ ADD THIS LINE
using Havit.Blazor.Components.Web.Bootstrap;  // <------ ADD THIS LINE
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpService = new HttpClient
                  {
                      BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
                  };

httpService.DefaultRequestHeaders.Add("Access-Control-Allow-Private-Network", "true");

builder.Services.AddScoped(_ => httpService);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
builder.Services.AddScoped<IHouseholdService, HouseholdService>();
builder.Services.AddScoped<IEconomyPlanService, EconomyPlanService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();

var options = builder.Configuration.Get<Options>();
if (options is null)
    throw new ApplicationException("Options is null");

builder.Services.AddSingleton(options);
builder.Services.AddSyncfusionBlazor();

builder.Services.AddBlazoredModal();
builder.Services.AddHxServices(); 

await builder.Build().RunAsync();