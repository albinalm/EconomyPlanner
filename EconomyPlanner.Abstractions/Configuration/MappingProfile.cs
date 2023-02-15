using AutoMapper;
using EconomyPlanner.Abstractions.Models;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.Abstractions.Configuration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ExpenseModel, Expense>().ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Expense, ExpenseModel>();
        
        CreateMap<IncomeModel, Income>().ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Income, IncomeModel>();

        CreateMap<RecurringExpense, Expense>().ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<RecurringIncome, Income>().ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<RecurringExpense, ExpenseModel>();
        CreateMap<RecurringIncome, IncomeModel>();
        
        CreateMap<IncomeModel, RecurringIncome>().ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<ExpenseModel, RecurringExpense>().ForMember(x => x.Id, opt => opt.Ignore());
        
        CreateMap<EconomyPlanModel, EconomyPlan>();
        CreateMap<EconomyPlan, EconomyPlanModel>().ForMember(x => x.Id, opt => opt.Ignore());

    }
}