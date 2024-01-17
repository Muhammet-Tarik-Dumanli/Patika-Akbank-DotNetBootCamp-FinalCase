using System.Data.Entity;
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
        var checkIdentity = await dbContext.Set<User>().Where(x => x.UserName == request.Model.UserName).FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity is not null)
            return new ApiResponse<UserResponse>($"{request.Model.UserName} is in use");

        var entity = mapper.Map<UserRequest, User>(request.Model);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<User, UserResponse>(entityResult.Entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found!");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found!");

        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Email = request.Model.Email;
        fromdb.Role = request.Model.Role;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}