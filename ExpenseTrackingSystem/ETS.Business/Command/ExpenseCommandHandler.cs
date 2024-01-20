using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Data.Enums;
using ETS.Schema;
using MediatR;
using static ETS.Business.CQRS.ExpenseCQRS;

namespace ETS.Business.Command
{
    public class ExpenseCommandHandler :
        IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
        IRequestHandler<UpdateExpenseCommand, ApiResponse>,
        IRequestHandler<DeleteExpenseCommand, ApiResponse>
    {
        private readonly ETSDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseCommandHandler(ETSDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = mapper.Map<ExpenseRequest, Expense>(request.Model);
                entity.Status = ExpenseStatus.Request;
                
                dbContext.Set<Expense>().Add(entity);
                await dbContext.SaveChangesAsync();

                var response = mapper.Map<Expense, ExpenseResponse>(entity);
                return new ApiResponse<ExpenseResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ExpenseResponse>($"Error creating expense: {ex.Message}");
            }
        }

        public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await dbContext.Set<Expense>().FindAsync(request.Id);

                if (entity == null)
                    return new ApiResponse("Expense not found!");

                if(entity.Status != ExpenseStatus.Request)
                    return new ApiResponse("Expenses that are not in request status cannot be updated!");

                mapper.Map(request.Model, entity);

                await dbContext.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Error updating expense: {ex.Message}");
            }
        }

        public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await dbContext.Set<Expense>().FindAsync(request.Id);

                if (entity == null)
                    return new ApiResponse("Expense not found!");

                dbContext.Set<Expense>().Remove(entity);

                await dbContext.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Error deleting expense: {ex.Message}");
            }
        }
    }
}
