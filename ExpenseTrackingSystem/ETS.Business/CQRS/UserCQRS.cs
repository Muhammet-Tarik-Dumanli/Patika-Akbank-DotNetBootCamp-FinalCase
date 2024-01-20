using ETS.Base.Response;
using ETS.Data.Enums;
using ETS.Schema;
using MediatR;

namespace ETS.Business.CQRS;

public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(int Id, UserRequest Model) : IRequest<ApiResponse>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;
public record GetApprovedAmount(DateTime startDate, DateTime endDate) : IRequest<ApiResponse>;

public record GetAllUsersQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;
public record GetUserByParameterQuery(string FirstName, string LastName, string UserName) : IRequest<ApiResponse<List<UserResponse>>>;
