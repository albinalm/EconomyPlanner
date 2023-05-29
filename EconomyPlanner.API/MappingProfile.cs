using AutoMapper;
using EconomyPlanner.Repository.Entities;

namespace EconomyPlanner.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RecurringExpense, Expense>().ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<RecurringIncome, Income>().ForMember(x => x.Id, opt => opt.Ignore());
    }
}