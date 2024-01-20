using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using MediatR;
using static ETS.Business.CQRS.ExpenseCategoryCQRS;

namespace ETS.Business.Query
{
    public class ExpenseCategoryQueryHandler :
        IRequestHandler<GetAllExpenseCategoriesQuery, ApiResponse<List<ExpenseCategoryResponse>>>,
        IRequestHandler<GetExpenseCategoryByIdQuery, ApiResponse<ExpenseCategoryResponse>>
    {
        private readonly ETSDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseCategoryQueryHandler(ETSDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
        {
            var list = dbContext.Set<ExpenseCategory>().ToList();

            var mappedList = mapper.Map<List<ExpenseCategory>, List<ExpenseCategoryResponse>>(list);
            return new ApiResponse<List<ExpenseCategoryResponse>>(mappedList);
        }

        public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = dbContext.Set<ExpenseCategory>().FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
                return new ApiResponse<ExpenseCategoryResponse>("Record not found!");

            var mapped = mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(entity);
            return new ApiResponse<ExpenseCategoryResponse>(mapped);
        }
    }
}
