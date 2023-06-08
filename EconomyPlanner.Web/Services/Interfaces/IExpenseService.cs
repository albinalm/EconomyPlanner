using EconomyPlanner.Web.Models;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;
using ExpenseModel = EconomyPlanner.Web.Models.ExpenseModel;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseModel>> ExpenseModels(EconomyPlanModel economyPlanModel);
    Task UpdateExpense(ExpenseModel expenseModel);
    Task<IEnumerable<string>> ExpenseTypes();
    Task DeleteExpense(ExpenseModel expenseModel, bool deleteRecurring);
    Task<bool> CheckIfExpenseIsRecurring(ExpenseModel expenseModel);
    Task AddExpense(CreateExpenseModel createExpenseModel);
    Task AddRecurringExpense(CreateExpenseModel createExpenseModel);
    Task UpdateRecurringExpense(ExpenseModel expenseModel);
    Task DeleteRecurringExpense(ExpenseModel expenseModel);
    Task AddRecurringExpenseAsExpense(ExpenseModel expenseModel, EconomyPlanModel economyPlanModel);
    Task<IEnumerable<ExpenseModel>> GetAllExpensesLinkedToRecurringExpense(ExpenseModel expenseModel);
    Task<ExpenseModel?> GetRecurringExpenseFromExpense(ExpenseModel expenseModel);
    Task<IEnumerable<ExpenseModel>> OneYearExpenseModels(string guid);
    Task<IEnumerable<ExpenseModel>> GetExpensesFromEconomyPlan(EconomyPlanModel economyPlanModel);
    void ClearCache();
}