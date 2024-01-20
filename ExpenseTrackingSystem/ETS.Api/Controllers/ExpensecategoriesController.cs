using ETS.Base.Response;
using ETS.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ETS.Business.CQRS.ExpenseCategoryCQRS;

namespace ETS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseCategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllExpenseCategories")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse<List<ExpenseCategoryResponse>>>> GetAllExpenseCategories()
        {
            var operation = new GetAllExpenseCategoriesQuery();
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpGet("GetExpenseCategoryById/{id}")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse<ExpenseCategoryResponse>>> GetExpenseCategoryById(int id)
        {
            var operation = new GetExpenseCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpPost("CreateExpenseCategory")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse<ExpenseCategoryResponse>>> CreateExpenseCategory([FromBody] ExpenseCategoryRequest expenseCategoryRequest)
        {
            var operation = new CreateExpenseCategoryCommand(expenseCategoryRequest);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpPut("UpdateExpenseCategory/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse>> UpdateExpenseCategory(int id, [FromBody] ExpenseCategoryRequest expenseCategoryRequest)
        {
            var operation = new UpdateExpenseCategoryCommand(id, expenseCategoryRequest);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpDelete("DeleteExpenseCategory/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse>> DeleteExpenseCategory(int id)
        {
            var operation = new DeleteExpenseCategoryCommand(id);
            var result = await mediator.Send(operation);
            return Ok(result);
        }
        
    }
}
