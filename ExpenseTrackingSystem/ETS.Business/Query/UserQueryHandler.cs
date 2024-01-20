using AutoMapper;
using ETS.Base.Response;
using ETS.Business.CQRS;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using LinqKit;
using MediatR;

namespace ETS.Business.Query;

public class UserQueryHandler :
    IRequestHandler<GetAllUsersQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>,
    IRequestHandler<GetUserByParameterQuery, ApiResponse<List<UserResponse>>>
    
{
    private readonly ETSDbContext dbContext;
    private readonly IMapper mapper;

    public UserQueryHandler(ETSDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var list =  dbContext.Set<User>().ToList();

        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  dbContext.Set<User>().FirstOrDefault(x => x.Id == request.Id);

        if(entity == null)
            return new ApiResponse<UserResponse>("Record not found!");

        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUserByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<User>(true);

        if(string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        if(string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        if(string.IsNullOrEmpty(request.UserName))
            predicate.And(x => x.UserName.ToUpper().Contains(request.UserName.ToUpper()));
        
        var list = dbContext.Set<User>().Where(predicate).ToList();

        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mappedList); 
    }
}
