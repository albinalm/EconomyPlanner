using EconomyPlanner.Web.Models;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IIncomeService
{
    Task<IEnumerable<IncomeModel>> GetIncomes(EconomyPlanModel economyPlanModel);
    Task UpdateIncome(IncomeModel incomeModel);
    Task<IEnumerable<string>> GetIncomeTypes();
    Task DeleteIncome(IncomeModel incomeModel, bool deleteRecurring);
    Task<bool> CheckIfIncomeIsRecurring(IncomeModel incomeModel);
    Task AddIncome(CreateIncomeModel createIncomeModel);
    Task AddRecurringIncome(CreateIncomeModel createIncomeModel);
    Task UpdateRecurringIncome(IncomeModel incomeModel);
    Task DeleteRecurringIncome(IncomeModel incomeModel);
    Task AddRecurringIncomeAsIncome(IncomeModel incomeModel, EconomyPlanModel economyPlanModel);
    Task<IEnumerable<IncomeModel>> GetAllIncomesLinkedToRecurringIncome(IncomeModel incomeModel);
    Task<IncomeModel?> GetRecurringIncomeFromIncome(IncomeModel incomeModel);
}