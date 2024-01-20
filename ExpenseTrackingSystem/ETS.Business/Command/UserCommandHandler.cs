using AutoMapper;
using ETS.Base.Response;
using ETS.Business.CQRS;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using MediatR;

namespace ETS.Business.Query;

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<DeleteUserCommand, ApiResponse>,
    IRequestHandler<UpdateUserCommand, ApiResponse>
{
    private readonly ETSDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(ETSDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = dbContext.Set<User>().Where(x => x.UserName == request.Model.UserName).FirstOrDefault();
        if (checkIdentity is not null)
            return new ApiResponse<UserResponse>($"{request.Model.UserName} is in use");

        var entity = mapper.Map<UserRequest, User>(request.Model);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        dbContext.SaveChanges();

        var mapped = mapper.Map<User, UserResponse>(entityResult.Entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = dbContext.Set<User>().Where(x => x.Id == request.Id).FirstOrDefault();

        if (fromdb is null)
            return new ApiResponse("Record not found!");

        fromdb.IsActive = false;
        dbContext.SaveChanges();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = dbContext.Set<User>().Where(x => x.Id == request.Id).FirstOrDefault();
        
        if(request.Model.Role == "personal" && request.Model.Id != fromdb.Id)
            return new ApiResponse("You do not have the necessary authorizations to perform this operation.!");

        if (fromdb is null)
            return new ApiResponse("Record not found!");

        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Email = request.Model.Email;
        fromdb.Role = request.Model.Role;

        dbContext.SaveChanges();
        return new ApiResponse();
    }
}
