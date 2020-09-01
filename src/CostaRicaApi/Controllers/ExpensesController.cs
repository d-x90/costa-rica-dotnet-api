using System.Collections.Generic;
using CostaRicaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostaRicaApi.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController: ControllerBase {

        private readonly ExpenseContext _context;
        public ExpensesController(ExpenseContext context) => _context = context;
        
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> Get() {
            return _context.Expenses;
        }
    }
}