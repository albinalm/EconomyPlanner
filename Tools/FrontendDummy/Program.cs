using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Enums;
using Newtonsoft.Json;
using RestSharp;

await AddExpense();
Thread.Sleep(1000);
await AddIncome();
Thread.Sleep(1000);
await AddEconomyPlan();
Thread.Sleep(1000);
Console.WriteLine("Finished!");
Console.ReadKey();



Expense GetExpense()
{
    return new Expense("TestExpense", 300, ExpenseType.Household, false, null);
}

ExpenseModel GetExpenseModelFromExpense()
{
    var expense = GetExpense();
    return new ExpenseModel(expense.Name, expense.Amount, expense.ExpenseType, expense.Recurring, expense.RecurringAmount);
}

Income GetIncome()
{
    return new Income("TestIncome", 300, IncomeType.Salary, false, null);
}

IncomeModel GetIncomeModelFromIncome()
{
    var income = GetIncome();
    return new IncomeModel(income.Name, income.Amount, income.IncomeType, income.Recurring, income.RecurringAmount);
}

EconomyPlanModel GetEconomyPlanModel()
{
    return new EconomyPlanModel("TestEconomyPlan2", new List<Expense> { GetExpense() }, new List<Income> { GetIncome() }, "2023-01-01", "2023-01-31");
}


async Task AddEconomyPlan()
{
    Console.WriteLine("Adding economy plan...");
    var strDataRest = JsonConvert.SerializeObject(GetEconomyPlanModel());

    var client = new RestClient("http://localhost:5179/");
    var request = new RestRequest("api/EconomyPlanner/AddEconomyPlan", Method.Post)
                  {
                      RequestFormat = DataFormat.Json
                  };

    request.AddBody(strDataRest);

    var response = await client.ExecuteAsync(request);

    if (response.IsSuccessful)
    {
        Console.WriteLine(response.Content);
    }
    else
    {
        Console.WriteLine(response.Content);
    }
}

async Task AddIncome()
{
    Console.WriteLine("Adding income...");
    var strDataRest = JsonConvert.SerializeObject(GetIncomeModelFromIncome());

    var client = new RestClient("http://localhost:5179/");
    var request = new RestRequest("api/Income/AddIncome", Method.Post)
                  {
                      RequestFormat = DataFormat.Json
                  };

    request.AddBody(strDataRest);

    var response = await client.ExecuteAsync(request);

    if (response.IsSuccessful)
    {
        Console.WriteLine(response.Content);
    }
    else
    {
        Console.WriteLine(response.Content);
    }
}

async Task AddExpense()
{
    Console.WriteLine("Adding expense...");
    var strDataRest = JsonConvert.SerializeObject(GetExpenseModelFromExpense());

    var client = new RestClient("http://localhost:5179/");
    var request = new RestRequest("api/Expense/AddExpense", Method.Post)
                  {
                      RequestFormat = DataFormat.Json
                  };

    request.AddBody(strDataRest);

    var response = await client.ExecuteAsync(request);

    if (response.IsSuccessful)
    {
        Console.WriteLine(response.Content);
    }
    else
    {
        Console.WriteLine(response.Content);
    }
}

