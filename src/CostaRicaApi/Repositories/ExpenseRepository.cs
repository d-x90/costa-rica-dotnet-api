using System.Collections.Generic;
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

        public Task<List<Expense>> GetAllExpensesAsync()
        {
            return _context.Expenses.ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int id)
        {
            return _context.Expenses.FindAsync(id).AsTask();
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