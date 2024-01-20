using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using LinqKit;
using MediatR;
using static ETS.Business.CQRS.ExpenseCQRS;

namespace ETS.Business.Query
{
    public class ExpenseQueryHandler :
        IRequestHandler<GetAllExpensesQuery, ApiResponse<List<ExpenseResponse>>>,
        IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>,
        IRequestHandler<GetExpensesByUserIdQuery, ApiResponse<List<ExpenseResponse>>>,
        IRequestHandler<GetExpensesByCategoryQuery, ApiResponse<List<ExpenseResponse>>>
    {
        private readonly ETSDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseQueryHandler(ETSDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            var list = dbContext.Set<Expense>().ToList();
            var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
            return new ApiResponse<List<ExpenseResponse>>(mappedList);
        }

        public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = dbContext.Set<Expense>().FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
                return new ApiResponse<ExpenseResponse>("Record not found!");

            var mapped = mapper.Map<Expense, ExpenseResponse>(entity);
            return new ApiResponse<ExpenseResponse>(mapped);
        }

        public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpensesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<Expense>(true);
            predicate.And(x => x.UserId == request.UserId);

            var list = dbContext.Set<Expense>().Where(predicate).ToList();
            var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
            return new ApiResponse<List<ExpenseResponse>>(mappedList);
        }

        public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpensesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<Expense>(true);
            predicate.And(x => x.CategoryId == request.CategoryId);

            var list = dbContext.Set<Expense>().Where(predicate).ToList();
            var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
            return new ApiResponse<List<ExpenseResponse>>(mappedList);
        }
    }
}
