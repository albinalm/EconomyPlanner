using AutoMapper;
using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Configuration;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Services;

public class HouseholdService : IHouseholdService
{
    private readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;

    public HouseholdService(DatabaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public HouseholdModel? GetHouseholdByGuid(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);

        return household is not null ? new HouseholdModel() { Guid = household.Guid } : null;
    }

    public void CreateHousehold(string name)
    {
        var household = Household.Create(name);
        
        _dbContext.Add(household);
        _dbContext.SaveChanges();
    }

    public IEnumerable<ExpenseModel> GetRecurringExpenses(string guid)
    {
        var household = _dbContext.GetHouseholdFromGuid(guid);

        if (household is null)
            throw new InvalidOperationException("HouseholdService > GetRecurringExpenses Household not found");

        return _mapper.Map<IEnumerable<ExpenseModel>>(household.RecurringExpenses);
    }
}