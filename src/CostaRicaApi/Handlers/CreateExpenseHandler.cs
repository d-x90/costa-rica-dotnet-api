using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostaRicaApi.Commands;
using CostaRicaApi.Dtos;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using MediatR;

namespace CostaRicaApi.Handlers {
    public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, ShallowExpenseDto>
    {
        private readonly IMapper _mapper;


        private readonly IUserRepository _userRepo;
        private readonly IExpenseRepository _expenseRepo;
        public CreateExpenseHandler(IMapper mapper, IUserRepository userRepo, IExpenseRepository expenseRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _expenseRepo = expenseRepo;
        }

        public async Task<ShallowExpenseDto> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = _mapper.Map<Expense>(request.Expense);
            expense.Owner = await _userRepo.GetUserByIdAsync(request.OwnerId);

            expense = await _expenseRepo.AddExpenseAsync(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return null;
            }

            return _mapper.Map<ShallowExpenseDto>(expense);
        }
    }
}