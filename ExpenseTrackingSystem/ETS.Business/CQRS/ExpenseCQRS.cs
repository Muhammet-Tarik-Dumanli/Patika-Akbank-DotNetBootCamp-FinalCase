using ETS.Base.Response;
using ETS.Schema;
using MediatR;

namespace ETS.Business.CQRS;

public class ExpenseCQRS
{
    public record CreateExpenseCommand(ExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;
    public record UpdateExpenseCommand(int Id, ExpenseRequest Model) : IRequest<ApiResponse>;
    public record DeleteExpenseCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllExpensesQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
    public record GetExpenseByIdQuery(int Id) : IRequest<ApiResponse<ExpenseResponse>>;
    public record GetExpensesByUserIdQuery(int UserId) : IRequest<ApiResponse<List<ExpenseResponse>>>;
    public record GetExpensesByCategoryQuery(int CategoryId) : IRequest<ApiResponse<List<ExpenseResponse>>>;
}
