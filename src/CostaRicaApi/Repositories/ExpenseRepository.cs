using System.Collections.Generic;
using CostaRicaApi.Models;

namespace CostaRicaApi.Repositories {
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseContext _context;

        public ExpenseRepository(ExpenseContext context) => _context = context; 

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _context.Expenses;
        }

        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.Find(id);
        }
    }
}