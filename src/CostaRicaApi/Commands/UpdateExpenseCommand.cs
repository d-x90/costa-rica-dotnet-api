using CostaRicaApi.Dtos;
using MediatR;

namespace CostaRicaApi.Commands {
    public class UpdateExpenseCommand : IRequest<bool?> {
        public int ExpenseId { get; set; }
        public ExpenseCreateUpdateDto Expense { get; set; }

        public UpdateExpenseCommand(int expenseId, ExpenseCreateUpdateDto expense)
        {
            ExpenseId = expenseId;
            Expense = expense;
        }
    }
}