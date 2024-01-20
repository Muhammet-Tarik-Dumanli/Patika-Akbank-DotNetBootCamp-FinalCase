using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using ETS.Base.Response;
using ETS.Business.CQRS;
using ETS.Schema;
using static ETS.Business.CQRS.ExpenseCQRS;

namespace ETS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpensesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllExpenses")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse<List<ExpenseResponse>>>> GetAllExpenses()
        {
            var operation = new GetAllExpensesQuery();
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse<ExpenseResponse>>> GetExpenseById(int id)
        {
            var operation = new GetExpenseByIdQuery(id);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpPost("CreateExpense")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse<ExpenseResponse>>> CreateExpense([FromBody] ExpenseRequest model)
        {
            var operation = new CreateExpenseCommand(model);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpPut("UpdateExpense/{id}")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse>> UpdateExpense(int id, [FromBody] ExpenseRequest model)
        {
            var operation = new UpdateExpenseCommand(id, model);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse>> DeleteExpense(int id)
        {
            var operation = new DeleteExpenseCommand(id);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpGet("GetExpensesByUserId/{userId}")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse<List<ExpenseResponse>>>> GetExpensesByUserId(int userId)
        {
            var operation = new GetExpensesByUserIdQuery(userId);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpGet("GetExpensesByCategoryId/{categoryId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse<List<ExpenseResponse>>>> GetExpensesByCategoryId(int categoryId)
        {
            var operation = new GetExpensesByCategoryQuery(categoryId);
            var result = await mediator.Send(operation);
            return Ok(result);
        }
        
    }
}
