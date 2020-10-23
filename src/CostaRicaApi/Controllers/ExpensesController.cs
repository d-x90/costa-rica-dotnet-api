using System.Threading.Tasks;
using CostaRicaApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using CostaRicaApi.Queries;
using MediatR;
using CostaRicaApi.Commands;

namespace CostaRicaApi.Controllers {

    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase {
        private readonly IMediator _mediator;

        private int GetCurrentUserId() => int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ListResponseDto<ShallowExpenseDto>>> GetExpenses() {
            var query = new GetAllExpensesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShallowExpenseDto>> GetExpense(int id) {
            var query = new GetExpenseByIdQuery(id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : (ActionResult) Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ShallowExpenseDto>> AddExpense(ExpenseCreateUpdateDto dto) {
            var command = new CreateExpenseCommand(dto, GetCurrentUserId());
            var result = await _mediator.Send(command);
            return result == null ? BadRequest() : (ActionResult) Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense(int id, ExpenseCreateUpdateDto dto) {
            var command = new UpdateExpenseCommand(id, dto);
            var result = await _mediator.Send(command);

            if(result == null) {
                return NotFound();
            } else if (result == false) {
                return BadRequest();
            }

            return NoContent();            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id) {
            var command = new DeleteExpenseCommand(id);
            var result = await _mediator.Send(command);

            if(result == null) {
                return NotFound();
            } else if(result == false) {
                return BadRequest();
            }

            return NoContent();
        }
    }
}