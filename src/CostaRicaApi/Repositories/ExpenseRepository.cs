using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaApi.Repositories {
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExpenseRepository(ExpenseContext context, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            var entrySet = await _context.Expenses.AddAsync(expense);

            return entrySet.Entity;
        }

        public Task<List<Expense>> GetAllExpensesAsync()
        {
            return _context.Expenses
                        .Include(x => x.Owner)
                        .Where(x => x.Owner != null && x.Owner.Id == GetCurrentUserId())
                        .OrderBy(x => x.Id)
                        .ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int id)
        {
            return _context.Expenses.Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == id && x.Owner.Id == GetCurrentUserId());
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