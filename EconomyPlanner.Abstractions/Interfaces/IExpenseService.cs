using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Abstractions.Interfaces;

public interface IExpenseService
{
    void CreateExpense(int economyPlanId,
                       string householdGuid,
                       string name,
                       decimal amount,
                       string expenseType,
                       bool isRecurring,
                       decimal recurringAmount);
    void UpdateExpenseFromModel(ExpenseModel expenseModel);
    ExpenseModel? GetExpenseModel(int expenseId);
    IEnumerable<string> GetExpenseTypes();
    void DeleteExpense(int expenseId, bool deleteRecurring);
    bool CheckIfExpenseIsRecurring(int expenseId);
    void CreateRecurringExpense(string householdGuid, string name, decimal amount, string expenseType);
    void UpdateRecurringExpenseFromModel(ExpenseModel expenseModel);
    void DeleteRecurringExpense(int recurringExpenseId);
    void AddRecurringExpenseAsExpense(int recurringExpenseId, int economyPlanId);
    IEnumerable<ExpenseModel> GetAllExpensesLinkedToRecurringExpense(int recurringExpenseId);
    ExpenseModel GetRecurringExpenseFromExpense(int expenseId);
}