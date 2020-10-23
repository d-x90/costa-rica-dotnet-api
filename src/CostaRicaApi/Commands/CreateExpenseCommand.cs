using CostaRicaApi.Dtos;
using MediatR;

namespace CostaRicaApi.Commands {
    public class CreateExpenseCommand : IRequest<ShallowExpenseDto> {
        public ExpenseCreateUpdateDto Expense { get; set; }
        public int OwnerId { get; set; }

        public CreateExpenseCommand(ExpenseCreateUpdateDto expense, int ownerId)
        {
            Expense = expense;
            OwnerId = ownerId;
        }
    }
}