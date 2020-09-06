using System.Collections.Generic;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CostaRicaApi.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController: ControllerBase {

        private readonly IExpenseRepository _expenseRepo;
        private readonly ILogger<ExpensesController> _logger;
        public ExpensesController(IExpenseRepository expenseRepo, ILogger<ExpensesController> logger) {
            _expenseRepo = expenseRepo;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> Get() {
            _logger.LogInformation("api/v1/expenses GET");

            return Ok(_expenseRepo.GetAllExpenses());
        }

        [HttpGet("{id}")]
        public ActionResult<Expense> Get(int id) {
            _logger.LogInformation("api/v1/expenses/{id} GET");

            var expense = _expenseRepo.GetExpenseById(id);

            if(expense == null) {
                return NotFound();
            }

            return Ok(expense);
        }
    }
}