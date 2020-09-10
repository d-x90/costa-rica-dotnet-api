using System.Collections.Generic;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaApi.Repositories {
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseContext _context;

        public ExpenseRepository(ExpenseContext context) => _context = context; 

        public Task<List<Expense>> GetAllExpensesAsync()
        {
            return _context.Expenses.ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int id)
        {
            return _context.Expenses.FindAsync(id).AsTask();
        }
    }
}