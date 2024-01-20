using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using LinqKit;
using MediatR;
using static ETS.Business.CQRS.PaymentCQRS;

namespace ETS.Business.Query
{
    public class PaymentQueryHandler :
        IRequestHandler<GetAllPaymentsQuery, ApiResponse<List<PaymentResponse>>>,
        IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>,
        IRequestHandler<GetPaymentsByUserIdQuery, ApiResponse<List<PaymentResponse>>>
    {
        private readonly ETSDbContext dbContext;
        private readonly IMapper mapper;

        public PaymentQueryHandler(ETSDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var list = dbContext.Set<Payment>().ToList();
            
            var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = dbContext.Set<Payment>().FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
                return new ApiResponse<PaymentResponse>("Record not found!");

            var mapped = mapper.Map<Payment, PaymentResponse>(entity);
            return new ApiResponse<PaymentResponse>(mapped);
        }

        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<Payment>(true);

            predicate.And(x => x.Id == request.UserId);

            var list = dbContext.Set<Payment>().Where(predicate).ToList();

            var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
        }
    }
}
