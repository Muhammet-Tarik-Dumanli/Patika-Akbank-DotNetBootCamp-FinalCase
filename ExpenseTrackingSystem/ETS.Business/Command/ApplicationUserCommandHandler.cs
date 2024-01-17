using System.Data.Entity;
using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using MediatR;
using static ETS.Business.CQRS.ApplicationUserCQRS;

namespace ETS.Business.Command;

public class ApplicationUserCommandHandler :
        IRequestHandler<CreateApplicationUserCommand, ApiResponse<ApplicationUserResponse>>,
        IRequestHandler<UpdateApplicationUserCommand, ApiResponse>,
        IRequestHandler<DeleteApplicationUserCommand, ApiResponse>
{
    private readonly ETSDbContext dbContext;
    private readonly IMapper mapper;

    public ApplicationUserCommandHandler(ETSDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ApplicationUserResponse>> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<ApplicationUser>().Where(x => x.UserName == request.Model.UserName).FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity is not null)
            return new ApiResponse<ApplicationUserResponse>($"{request.Model.UserName} is in use");

        var entity = mapper.Map<ApplicationUserRequest, ApplicationUser>(request.Model);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ApplicationUser, ApplicationUserResponse>(entityResult.Entity);
        return new ApiResponse<ApplicationUserResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ApplicationUser>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found!");

        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Email = request.Model.Email;
        fromdb.Role = request.Model.Role;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }

    public async Task<ApiResponse> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ApplicationUser>().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found!");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}