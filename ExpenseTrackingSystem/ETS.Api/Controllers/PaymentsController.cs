using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ETS.Base.Response;
using ETS.Schema;
using static ETS.Business.CQRS.PaymentCQRS;

namespace ETS.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllPayments")]
        public async Task<ActionResult<ApiResponse<List<PaymentResponse>>>> GetAllPayments()
        {
            var operation = new GetAllPaymentsQuery();
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpGet("GetPaymentById/{id}")]
        public async Task<ActionResult<ApiResponse<PaymentResponse>>> GetPaymentById(int id)
        {
            var operation = new GetPaymentByIdQuery(id);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpGet("GetPaymentsByUserId/{userId}")]
        [Authorize(Roles = "admin, personal")]
        public async Task<ActionResult<ApiResponse<List<PaymentResponse>>>> GetPaymentsByUserId(int userId)
        {
            try
            {
                var operation = new GetPaymentsByUserIdQuery(userId);
                var result = await mediator.Send(operation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<PaymentResponse>>($"Error getting payments: {ex.Message}"));
            }
        }


        [HttpPost("CreatePayment")]
        public async Task<ActionResult<ApiResponse<PaymentResponse>>> CreatePayment([FromBody] PaymentRequest model)
        {
            var operation = new CreatePaymentCommand(model);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpPut("UpdatePayment/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdatePayment(int id, [FromBody] PaymentRequest model)
        {
            var operation = new UpdatePaymentCommand(id, model);
            var result = await mediator.Send(operation);
            return Ok(result);
        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePayment(int id)
        {
            var operation = new DeletePaymentCommand(id);
            var result = await mediator.Send(operation);
            return Ok(result);
        }
    }
}
