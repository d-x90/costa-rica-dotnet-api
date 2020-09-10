using System.Collections.Generic;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaApi.Repositories {
    public interface IExpenseRepository {
        Task<List<Expense>> GetAllExpensesAsync();
        
        Task<Expense> GetExpenseByIdAsync(int id);
    }
}