using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IExpenseService
{
    void CreateExpense(int economyPlanId, string householdGuid, string name, decimal amount, string expenseType, bool isRecurring);
    void UpdateExpenseFromModel(ExpenseModel expenseModel);
    ExpenseModel? GetExpenseModel(int expenseId);
    IEnumerable<string> GetExpenseTypes();
    void DeleteExpense(int expenseId, bool deleteRecurring);
    bool CheckIfExpenseIsRecurring(int expenseId);
}