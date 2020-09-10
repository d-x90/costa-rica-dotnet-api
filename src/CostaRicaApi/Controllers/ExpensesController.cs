using System.Collections.Generic;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using CostaRicaApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace CostaRicaApi.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase {

        private readonly IExpenseRepository _expenseRepo;
        private readonly IMapper _mapper;
        public ExpensesController(IExpenseRepository expenseRepo, IMapper mapper)
        {
            _expenseRepo = expenseRepo;
            _mapper = mapper;
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

        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(ExpenseCreateUpdateDto dto) {
            var expense = _mapper.Map<Expense>(dto);
            expense = await _expenseRepo.AddExpenseAsync(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return BadRequest();
            }

            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense(int id, ExpenseCreateUpdateDto dto) {
            var expense = await _expenseRepo.GetExpenseByIdAsync(id);

            if(expense == null) {
                return NotFound();
            }

            _mapper.Map(dto, expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id) {
            var expense = await _expenseRepo.GetExpenseByIdAsync(id);

            if(expense == null) {
                return NotFound();
            }

            _expenseRepo.RemoveExpense(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return BadRequest();
            }

            return NoContent();
        }
    }
}