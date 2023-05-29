using EconomyPlanner.Abstractions.Interfaces;
using EconomyPlanner.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EconomyPlanner.Abstractions.Services;

public class TimeService : ITimeService
{
    private readonly DatabaseContext _dbContext;
    
    private DateTime? _cachedDate;
    
    public TimeService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DateTime GetNow()
    {
        return _cachedDate ??= _dbContext.Database.SqlQuery<DateTime>($"SELECT getdate()").AsEnumerable().First();
    }
}