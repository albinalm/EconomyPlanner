using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IEconomyPlanService
{
    Task<IEnumerable<EconomyPlanModel>> GetEconomyPlans();
    Task SetupActiveEconomyPlans();
}