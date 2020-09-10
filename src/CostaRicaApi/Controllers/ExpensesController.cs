using System.Collections.Generic;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using CostaRicaApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace CostaRicaApi.Controllers {

    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase {

        private readonly IExpenseRepository _expenseRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ExpensesController> _logger;
        public ExpensesController(IExpenseRepository expenseRepo, IMapper mapper, IUserRepository userRepo, ILogger<ExpensesController> logger)
        {
            _expenseRepo = expenseRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShallowExpenseDto>>> GetExpenses() {
            var currentUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var expenses = await _expenseRepo.GetAllExpensesAsync(currentUserId);

            return expenses.Select(x => _mapper.Map<ShallowExpenseDto>(x)).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShallowExpenseDto>> GetExpense(int id) {
            var currentUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var expense = await _expenseRepo.GetExpenseByIdAsync(id);
            
            if(expense == null) {
                return NotFound();
            }

            var expenseDto = _mapper.Map<ShallowExpenseDto>(expense);

            if(expenseDto.OwnerId != currentUserId) {
                return Unauthorized();
            }

            return Ok(expenseDto);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(ExpenseCreateUpdateDto dto) {
            var currentUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var expense = _mapper.Map<Expense>(dto);
            expense.Owner = await _userRepo.GetUserByIdAsync(currentUserId);

            expense = await _expenseRepo.AddExpenseAsync(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return BadRequest();
            }

            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense(int id, ExpenseCreateUpdateDto dto) {
            var currentUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var expense = await _expenseRepo.GetExpenseByIdAsync(id);

            if(expense.Owner.Id != currentUserId) {
                return Unauthorized();
            }

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
            var currentUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var expense = await _expenseRepo.GetExpenseByIdAsync(id);

            if(expense == null) {
                return NotFound();
            }

            if(expense.Owner.Id != currentUserId) {
                return Unauthorized();
            }

            _expenseRepo.RemoveExpense(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return BadRequest();
            }

            return NoContent();
        }
    }
}