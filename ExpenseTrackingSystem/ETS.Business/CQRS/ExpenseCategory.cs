using ETS.Base.Response;
using ETS.Schema;
using MediatR;

namespace ETS.Business.CQRS;

public class ExpenseCategoryCQRS
{
    public record CreateExpenseCategoryCommand(ExpenseCategoryRequest Model) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
    public record UpdateExpenseCategoryCommand(int Id, ExpenseCategoryRequest Model) : IRequest<ApiResponse>;
    public record DeleteExpenseCategoryCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllExpenseCategoriesQuery() : IRequest<ApiResponse<List<ExpenseCategoryResponse>>>;
    public record GetExpenseCategoryByIdQuery(int Id) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
}
