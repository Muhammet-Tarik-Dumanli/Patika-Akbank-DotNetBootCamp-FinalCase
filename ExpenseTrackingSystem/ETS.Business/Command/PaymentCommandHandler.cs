using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Data.Enums;
using ETS.Schema;
using MediatR;
using static ETS.Business.CQRS.PaymentCQRS;

namespace ETS.Business.Command
{
    public class PaymentCommandHandler :
        IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>,
        IRequestHandler<UpdatePaymentCommand, ApiResponse>,
        IRequestHandler<DeletePaymentCommand, ApiResponse>
    {
        private readonly ETSDbContext dbContext;
        private readonly IMapper mapper;

        public PaymentCommandHandler(ETSDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<PaymentRequest, Payment>(request.Model);
                dbContext.Set<Payment>().Add(entity);
                dbContext.SaveChanges();

                var response = mapper.Map<Payment, PaymentResponse>(entity);

                var expense = await dbContext.Set<Expense>().FindAsync(request.Model.ExpenseId);
                if (expense != null)
                {
                    expense.Status = ExpenseStatus.Approved;
                    dbContext.SaveChanges();
                }

                return new ApiResponse<PaymentResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaymentResponse>($"Error creating payment: {ex.Message}");
            }
        }

        public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await dbContext.Set<Payment>().FindAsync(request.Id);

                if (entity == null)
                    return new ApiResponse("Payment not found!");

                mapper.Map(request.Model, entity);

                await dbContext.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Error updating payment: {ex.Message}");
            }
        }

        public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await dbContext.Set<Payment>().FindAsync(request.Id);

                if (entity == null)
                    return new ApiResponse("Payment not found!");

                dbContext.Set<Payment>().Remove(entity);

                await dbContext.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Error deleting payment: {ex.Message}");
            }
        }
    }
}
