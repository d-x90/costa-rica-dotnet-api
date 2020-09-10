using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaApi.Repositories {
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseContext _context;

        public ExpenseRepository(ExpenseContext context) => _context = context;

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            var entrySet = await _context.Expenses.AddAsync(expense);

            return entrySet.Entity;
        }

        public Task<List<Expense>> GetAllExpensesAsync(int ownerId)
        {
            return _context.Expenses
                        .Include(x => x.Owner)
                        .Where(x => x.Owner != null && x.Owner.Id == ownerId)
                        .OrderBy(x => x.Id)
                        .ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int id)
        {
            return _context.Expenses.Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == id);
        }

        public void RemoveExpense(Expense expense)
        {
            _context.Expenses.Remove(expense);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}