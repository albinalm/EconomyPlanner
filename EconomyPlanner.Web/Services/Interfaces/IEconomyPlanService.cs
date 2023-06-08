using EconomyPlanModel = EconomyPlanner.Web.Models.EconomyPlanModel;

namespace EconomyPlanner.Web.Services.Interfaces;

public interface IEconomyPlanService
{
    Task<IEnumerable<EconomyPlanModel>> EconomyPlanModels();
    Task SetupActiveEconomyPlans();
    Task<IEnumerable<EconomyPlanModel>> TryGetOneYearEconomyPlanModels(string guid);
    void ClearCache();
}