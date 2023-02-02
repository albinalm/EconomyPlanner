using EconomyPlanner.Abstractions.Models;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IEconomyPlanService
{
    Task<IEnumerable<EconomyPlanModel>> GetEconomyPlans();
}