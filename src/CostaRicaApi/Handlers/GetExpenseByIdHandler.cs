using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostaRicaApi.Dtos;
using CostaRicaApi.Queries;
using CostaRicaApi.Repositories;
using MediatR;

namespace CostaRicaApi.Handlers {
    public class GetExpenseByIdHandler : IRequestHandler<GetExpenseByIdQuery, ShallowExpenseDto>
    {
        private readonly IExpenseRepository _expenseRepo;
        private readonly IMapper _mapper;

        public GetExpenseByIdHandler(IExpenseRepository expenseRepo, IMapper mapper)
        {
            _expenseRepo = expenseRepo;
            _mapper = mapper;
        }

        public async Task<ShallowExpenseDto> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepo.GetExpenseByIdAsync(request.Id);

            return expense != null ? _mapper.Map<ShallowExpenseDto>(expense) : null;
        }
    }
}