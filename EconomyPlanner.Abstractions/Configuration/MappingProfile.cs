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
    }
}