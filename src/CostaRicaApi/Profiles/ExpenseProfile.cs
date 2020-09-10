using AutoMapper;
using CostaRicaApi.Dtos;
using CostaRicaApi.Models;

namespace CostaRicaApi.Profiles {
    public class ExpenseProfile : Profile {
        public ExpenseProfile()
        {
            CreateMap<ExpenseCreateUptadeDto, Expense>();
        }
    }
}