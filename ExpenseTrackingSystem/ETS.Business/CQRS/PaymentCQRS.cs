using ETS.Base.Response;
using ETS.Schema;
using MediatR;

namespace ETS.Business.CQRS;

public class PaymentCQRS
{
    public record CreatePaymentCommand(PaymentRequest Model) : IRequest<ApiResponse<PaymentResponse>>;
    public record UpdatePaymentCommand(int Id, PaymentRequest Model) : IRequest<ApiResponse>;
    public record DeletePaymentCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllPaymentsQuery() : IRequest<ApiResponse<List<PaymentResponse>>>;
    public record GetPaymentByIdQuery(int Id) : IRequest<ApiResponse<PaymentResponse>>;
    public record GetPaymentsByUserIdQuery(int UserId) : IRequest<ApiResponse<List<PaymentResponse>>>;
}
