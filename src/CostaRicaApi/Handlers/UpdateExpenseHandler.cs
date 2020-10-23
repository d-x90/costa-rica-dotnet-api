using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostaRicaApi.Commands;
using CostaRicaApi.Dtos;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using MediatR;

namespace CostaRicaApi.Handlers {
    public class UpdateExpenseHandler : IRequestHandler<UpdateExpenseCommand, bool?>
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepo;
        public UpdateExpenseHandler(IMapper mapper, IUserRepository userRepo, IExpenseRepository expenseRepo)
        {
            _mapper = mapper;
            _expenseRepo = expenseRepo;
        }

        async Task<bool?> IRequestHandler<UpdateExpenseCommand, bool?>.Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expenseFromDb = await _expenseRepo.GetExpenseByIdAsync(request.ExpenseId);

            if(expenseFromDb == null) {
                return null;
            }

            _mapper.Map(request.Expense, expenseFromDb);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return false;
            }

            return true;
        }
    }
}