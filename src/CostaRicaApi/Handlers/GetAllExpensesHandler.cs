using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostaRicaApi.Dtos;
using CostaRicaApi.Queries;
using CostaRicaApi.Repositories;
using MediatR;

namespace CostaRicaApi.Handlers {
    public class GetAllExpensesHandler : IRequestHandler<GetAllExpensesQuery, ListResponseDto<ShallowExpenseDto>>
    {
        private readonly IExpenseRepository _expenseRepo;
        private readonly IMapper _mapper;

        public GetAllExpensesHandler(IExpenseRepository expenseRepo, IMapper mapper)
        {
            _expenseRepo = expenseRepo;
            _mapper = mapper;
        }

        public async Task<ListResponseDto<ShallowExpenseDto>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepo.GetAllExpensesAsync();

            return new ListResponseDto<ShallowExpenseDto>() { Items = expenses.Select(x => _mapper.Map<ShallowExpenseDto>(x)).ToList() };
        }
    }
}