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

        private int GetCurrentUserId() => int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ExpensesController(IExpenseRepository expenseRepo, IMapper mapper, IUserRepository userRepo, ILogger<ExpensesController> logger)
        {
            _expenseRepo = expenseRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ListResponseDto<ShallowExpenseDto>>> GetExpenses() {
            var expenses = await _expenseRepo.GetAllExpensesAsync();

            return new ListResponseDto<ShallowExpenseDto>() { Items = expenses.Select(x => _mapper.Map<ShallowExpenseDto>(x)).ToList() };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShallowExpenseDto>> GetExpense(int id) {
            var expense = await _expenseRepo.GetExpenseByIdAsync(id);
            
            if(expense == null) {
                return NotFound();
            }

            var expenseDto = _mapper.Map<ShallowExpenseDto>(expense);

            return Ok(expenseDto);
        }

        [HttpPost]
        public async Task<ActionResult<ShallowExpenseDto>> AddExpense(ExpenseCreateUpdateDto dto) {
            var expense = _mapper.Map<Expense>(dto);
            expense.Owner = await _userRepo.GetUserByIdAsync(GetCurrentUserId());

            expense = await _expenseRepo.AddExpenseAsync(expense);

            if(0 == await _expenseRepo.SaveChangesAsync()) {
                return BadRequest();
            }

            return Ok(_mapper.Map<ShallowExpenseDto>(expense));
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