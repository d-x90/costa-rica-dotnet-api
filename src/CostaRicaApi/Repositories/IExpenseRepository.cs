using System.Collections.Generic;
using CostaRicaApi.Models;

namespace CostaRicaApi.Repositories {
    public interface IExpenseRepository {
        IEnumerable<Expense> GetAllExpenses();
        
        Expense GetExpenseById(int id);
    }
}