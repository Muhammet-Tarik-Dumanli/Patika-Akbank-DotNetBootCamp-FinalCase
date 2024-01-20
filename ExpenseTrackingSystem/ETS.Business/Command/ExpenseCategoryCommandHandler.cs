using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Data.Enums;
using ETS.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ETS.Business.CQRS.ExpenseCategoryCQRS;

namespace ETS.Business.Command
{
    public class ExpenseCategoryCommandHandler :
        IRequestHandler<CreateExpenseCategoryCommand, ApiResponse<ExpenseCategoryResponse>>,
        IRequestHandler<UpdateExpenseCategoryCommand, ApiResponse>,
        IRequestHandler<DeleteExpenseCategoryCommand, ApiResponse>
    {
        private readonly ETSDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseCategoryCommandHandler(ETSDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ExpenseCategoryRequest, ExpenseCategory>(request.Model);
            
            dbContext.Set<ExpenseCategory>().Add(entity);
            await dbContext.SaveChangesAsync();

            var response = mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(entity);
            return new ApiResponse<ExpenseCategoryResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<ExpenseCategory>().FindAsync(request.Id);

            if (entity == null)
                return new ApiResponse("Expense category not found!");

            entity.CategoryName = request.Model.CategoryName;
            entity.Description = request.Model.Description;

            await dbContext.SaveChangesAsync();

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<ExpenseCategory>().FindAsync(request.Id);

            if (entity == null)
                return new ApiResponse("Expense category not found!");

            var hasUnpaidExpenses = await dbContext.Set<Expense>().AnyAsync(e => e.CategoryId == request.Id && e.Status == ExpenseStatus.Request);

            if (hasUnpaidExpenses)
                return new ApiResponse("There are unpaid expenses associated with this category. It cannot be deleted.");

            dbContext.Set<ExpenseCategory>().Remove(entity);

            await dbContext.SaveChangesAsync();

            return new ApiResponse();
        }
    }
}
