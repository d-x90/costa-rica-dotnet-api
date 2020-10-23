using CostaRicaApi.Dtos;
using MediatR;

namespace CostaRicaApi.Commands {
    public class DeleteExpenseCommand : IRequest<bool?> {
        public int ExpenseId { get; set; }

        public DeleteExpenseCommand(int expenseId)
        {
            ExpenseId = expenseId;
        }
    }
}