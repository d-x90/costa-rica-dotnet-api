using System.Collections.Generic;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CostaRicaApi.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase {

        private readonly IExpenseRepository _expenseRepo;
        public ExpensesController(IExpenseRepository expenseRepo) {
            _expenseRepo = expenseRepo;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetExpenses() {
            var expenses = await _expenseRepo.GetAllExpensesAsync();

            return expenses;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id) {
            var expense = await _expenseRepo.GetExpenseByIdAsync(id);

            if(expense == null) {
                return NotFound();
            }

            return Ok(expense);
        }
    }
}