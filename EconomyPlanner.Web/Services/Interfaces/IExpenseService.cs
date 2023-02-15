using EconomyPlanner.Web.Models;
using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;
using ExpenseModel = EconomyPlanner.Web.Models.ExpenseModel;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IExpenseService
{
    IEnumerable<ExpenseModel> GetExpenses(EconomyPlanModel economyPlanModel);
    Task UpdateExpense(ExpenseModel expenseModel);
    Task<IEnumerable<string>> GetExpenseTypes();
    Task DeleteExpense(ExpenseModel expenseModel, bool deleteRecurring);
    Task<bool> CheckIfExpenseIsRecurring(ExpenseModel expenseModel);
    Task AddExpense(CreateExpenseModel createExpenseModel);
    Task AddRecurringExpense(CreateExpenseModel createExpenseModel);
    Task UpdateRecurringExpense(ExpenseModel expenseModel);
    Task DeleteRecurringExpense(ExpenseModel expenseModel);
}