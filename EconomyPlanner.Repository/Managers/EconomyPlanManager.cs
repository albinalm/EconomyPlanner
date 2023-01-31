using EconomyPlanner.Repository.Entities;
using EconomyPlanner.Repository.Helpers;
using EconomyPlanner.Repository.Managers.Interfaces;

namespace EconomyPlanner.Repository.Managers;

public class EconomyPlanManager : IEconomyPlanManager
{
    public EconomyPlan Create(string name, ICollection<Expense>? expenses, ICollection<Income>? incomes)
    {
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;

        var startDate = $"{year:yyyy}-" +
                        $"{month:MM}-01";
        
        var endDate = $"{year:yyyy}-" +
                      $"{month:MM}-" +
                      $"{DateTime.DaysInMonth(year, month)}";

        return new EconomyPlan(name,
                               startDate,
                               endDate)
               {
                   Expenses = expenses ?? Collection.Empty<Expense>(),
                   Incomes = incomes ?? Collection.Empty<Income>()
               };
        
    }
}