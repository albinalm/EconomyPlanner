using EconomyPlanner.API.TransactionTypes;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyPlanner.API.Services;

public class ExpenseService : IExpenseService
{
    private readonly DatabaseContext _dbContext;

    public ExpenseService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateExpense(int economyPlanId,
                              string householdGuid,
                              string name,
                              decimal amount,
                              string expenseType,
                              bool isRecurring,
                              decimal recurringAmount)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);

        if (economyPlan == null || household == null)
            return;

        var expense = Expense.Create(name, amount, expenseType, null);

        if (isRecurring)
        {
            var recurringExpense = RecurringExpense.Create(name, recurringAmount, expenseType);

            _dbContext.Add(recurringExpense);

            household.RecurringExpenses.Add(recurringExpense);
            expense.SetRecurringExpense(recurringExpense);
        }

        _dbContext.Expenses.Add(expense);
        economyPlan.Expenses.Add(expense);
        _dbContext.EconomyPlans.Update(economyPlan);
        _dbContext.Households.Update(household);
        _dbContext.SaveChanges();
    }

    public void CreateRecurringExpense(string householdGuid, string name, decimal amount, string expenseType)
    {
        var household = _dbContext.GetHouseholdFromGuid(householdGuid);

        if (household == null)
            return;

        var recurringExpense = RecurringExpense.Create(name, amount, expenseType);

        _dbContext.Add(recurringExpense);

        household.RecurringExpenses.Add(recurringExpense);

        _dbContext.Households.Update(household);
        _dbContext.SaveChanges();
    }

    public void UpdateExpense(Expense expense)
    {
        _dbContext.Update(expense);
        _dbContext.SaveChanges();
    }

    public void UpdateRecurringExpense(RecurringExpense recurringExpense)
    {
        _dbContext.Update(recurringExpense);
        _dbContext.SaveChanges();
    }

    public Expense? GetExpense(int expenseId)
    {
        return _dbContext.Expenses.Find(expenseId);
    }

    public IEnumerable<string> GetExpenseTypes()
    {
        return ExpenseType.GetExpenseTypes();
    }

    public void DeleteExpense(int expenseId, bool deleteRecurring)
    {
        var expense = _dbContext.Expenses.Where(e => e.Id == expenseId)
                                .Include(e => e.RecurringExpense)
                                .FirstOrDefault();

        if (expense is null)
            return;

        if (deleteRecurring && expense.RecurringExpense != null)
        {
            var recurringExpense = expense.RecurringExpense;
            
            var linkedExpenses = GetAllExpensesLinkedToRecurringExpense(expense.RecurringExpense);
            if (linkedExpenses.Any())
            {
                foreach (var linkedExpense in linkedExpenses)
                {
                    linkedExpense.RecurringExpense = null;
                }
            }
            _dbContext.RecurringExpenses.Remove(recurringExpense); 
        }
            

        _dbContext.Expenses.Remove(expense);
        _dbContext.SaveChanges();
    }

    public void DeleteRecurringExpense(int recurringExpenseId)
    {
        var recurringExpense = _dbContext.RecurringExpenses.Find(recurringExpenseId);

        if (recurringExpense is null)
            return;
        
        var linkedExpenses = GetAllExpensesLinkedToRecurringExpense(recurringExpense);
        if (linkedExpenses.Any())
        {
            foreach (var linkedExpense in linkedExpenses)
            {
                linkedExpense.RecurringExpense = null;
            }
        }
        
        _dbContext.RecurringExpenses.Remove(recurringExpense);
        _dbContext.SaveChanges();
    }

    private List<Expense> GetAllExpensesLinkedToRecurringExpense(RecurringExpense recurringExpense)
    {
        return _dbContext.Expenses.Where(e => e.RecurringExpense == recurringExpense).ToList();
    }
    
    public bool CheckIfExpenseIsRecurring(int expenseId)
    {
        var expense = _dbContext.Expenses.Where(e => e.Id == expenseId)
                                .Include(e => e.RecurringExpense)
                                .FirstOrDefault();

        return expense?.RecurringExpense != null;
    }

    public void AddRecurringExpenseAsExpense(int recurringExpenseId, int economyPlanId)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(economyPlanId);
        var recurringExpense = _dbContext.RecurringExpenses.Find(recurringExpenseId);

        if (economyPlan == null || recurringExpense == null)
            return;

        var expense = Expense.Create(recurringExpense.Name, recurringExpense.Amount, recurringExpense.ExpenseType, null);
        expense.RecurringExpense = recurringExpense;
        
        _dbContext.Expenses.Add(expense);
        economyPlan.Expenses.Add(expense);
        _dbContext.EconomyPlans.Update(economyPlan);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Expense> GetAllExpensesLinkedToRecurringExpense(int recurringExpenseId)
    {
        var recurringExpense = _dbContext.RecurringExpenses.Find(recurringExpenseId);
        return _dbContext.Expenses.Where(e => e.RecurringExpense == recurringExpense).ToList();
    }

    public RecurringExpense? GetRecurringExpenseFromExpense(int expenseId)
    {
        return _dbContext.Expenses.Where(e => e.Id == expenseId).Include(e => e.RecurringExpense).FirstOrDefault()?.RecurringExpense;
    }
    
    public IEnumerable<RecurringExpense> GetRecurringExpensesFromHouseholdGuid(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);

        if (household is null)
            throw new InvalidOperationException("ExpenseService > GetRecurringExpensesFromHouseholdGuid Household not found");

        return household.RecurringExpenses;
    }
    
    public IEnumerable<Expense> GetExpensesFromEconomyPlan(int id)
    {
        var economyPlan = _dbContext.GetEconomyPlanFromId(id);

        if (economyPlan is null)
            throw new InvalidOperationException("ExpenseService > GetExpensesFromEconomyPlan EconomyPlan not found");

        return economyPlan.Expenses;
    }

    public IEnumerable<Expense> GetAllExpensesFromLastSixEconomyPlans(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);
        
        if (household is null)
            throw new InvalidOperationException("ExpenseService > GetRecurringExpensesFromHouseholdGuid Household not found");
        
        var economyPlans = _dbContext.GetEconomyPlansFromHousehold(household)?.ToList();
        
        if (economyPlans is null || !economyPlans.Any())
        {
            return Enumerable.Empty<Expense>();
        }

        var latestEconomyPlans = economyPlans.OrderBy(ep => DateTime.Parse(ep.EndDate)).TakeLast(6).ToList();
        
        var totalExpenses = new List<Expense>();
        
        latestEconomyPlans.ForEach(ep =>
        {
            totalExpenses.AddRange(ep.Expenses);
        });
        
        return totalExpenses;
    }
}