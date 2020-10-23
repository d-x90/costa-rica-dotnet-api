using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostaRicaApi.Commands;
using CostaRicaApi.Dtos;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using MediatR;

namespace CostaRicaApi.Handlers {
    public class DeleteExpenseHandler : IRequestHandler<DeleteExpenseCommand, bool?>
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepo;
        public DeleteExpenseHandler(IMapper mapper, IUserRepository userRepo, IExpenseRepository expenseRepo)
        {
            _mapper = mapper;
            _expenseRepo = expenseRepo;
        }

        async Task<bool?> IRequestHandler<DeleteExpenseCommand, bool?>.Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepo.GetExpenseByIdAsync(request.ExpenseId);

            if(expense == null) {
                return null;
            }

            _expenseRepo.RemoveExpense(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return false;
            }

            return true;
        }
    }
}