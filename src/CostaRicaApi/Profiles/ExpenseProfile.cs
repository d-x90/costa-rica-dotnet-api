using AutoMapper;
using CostaRicaApi.Dtos;
using CostaRicaApi.Models;

namespace CostaRicaApi.Profiles {
    public class ExpenseProfile : Profile {
        public ExpenseProfile()
        {
            CreateMap<ExpenseCreateUpdateDto, Expense>();
            CreateMap<Expense, ExpenseCreateUpdateDto>();
            CreateMap<Expense, ShallowExpenseDto>()
                .ForMember(x => x.OwnerId, opt => opt.MapFrom(source => source.Owner.Id));
        }
    }
}