using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API.Services;

public interface IExpenseService
{
    void CreateExpense(int economyPlanId,
                       string householdGuid,
                       string name,
                       decimal amount,
                       string expenseType,
                       bool isRecurring,
                       decimal recurringAmount);

    void CreateRecurringExpense(string householdGuid, string name, decimal amount, string expenseType);
    void UpdateExpense(Expense expense);
    void UpdateRecurringExpense(RecurringExpense recurringExpense);
    Expense? GetExpense(int expenseId);
    IEnumerable<string> GetExpenseTypes();
    void DeleteExpense(int expenseId, bool deleteRecurring);
    void DeleteRecurringExpense(int recurringExpenseId);
    IEnumerable<Expense> GetAllExpensesLinkedToRecurringExpense(int recurringExpenseId);
    bool CheckIfExpenseIsRecurring(int expenseId);
    void AddRecurringExpenseAsExpense(int recurringExpenseId, int economyPlanId);
    RecurringExpense? GetRecurringExpenseFromExpense(int expenseId);
    IEnumerable<RecurringExpense> GetRecurringExpensesFromHouseholdGuid(string guid);
    IEnumerable<Expense> GetExpensesFromEconomyPlan(int id);
    IEnumerable<Expense> GetAllExpensesFromLastYearEconomyPlans(string guid);
}