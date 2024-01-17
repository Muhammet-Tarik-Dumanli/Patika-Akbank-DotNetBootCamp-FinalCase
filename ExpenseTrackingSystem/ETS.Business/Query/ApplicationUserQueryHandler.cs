using System.Data.Entity;
using AutoMapper;
using ETS.Base.Response;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using LinqKit;
using MediatR;
using static ETS.Business.CQRS.ApplicationUserCQRS;

namespace ETS.Business.Query;

public class ApplicationUserQueryHandler :
    IRequestHandler<GetAllApplicationUserQuery, ApiResponse<List<ApplicationUserResponse>>>,
    IRequestHandler<GetApplicationUserByIdQuery, ApiResponse<ApplicationUserResponse>>,
    IRequestHandler<GetApplicationUserByParameterQuery, ApiResponse<List<ApplicationUserResponse>>>
{
    private readonly ETSDbContext dbContext;
    private readonly IMapper mapper;
    public ApplicationUserQueryHandler(ETSDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<ApplicationUserResponse>>> Handle(GetAllApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ApplicationUser>().ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ApplicationUser>, List<ApplicationUserResponse>>(list);
        return new ApiResponse<List<ApplicationUserResponse>>(mappedList);
    }

    public async Task<ApiResponse<ApplicationUserResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if(entity == null)
            return new ApiResponse<ApplicationUserResponse>("Record not found!");

        var mapped = mapper.Map<ApplicationUser, ApplicationUserResponse>(entity);
        return new ApiResponse<ApplicationUserResponse>(mapped);
    }

    public async Task<ApiResponse<List<ApplicationUserResponse>>> Handle(GetApplicationUserByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ApplicationUser>(true);

        if(string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        if(string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        if(string.IsNullOrEmpty(request.UserName))
            predicate.And(x => x.UserName.ToUpper().Contains(request.UserName.ToUpper()));
        
        var list =  await dbContext.Set<ApplicationUser>().Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<ApplicationUser>, List<ApplicationUserResponse>>(list);
        return new ApiResponse<List<ApplicationUserResponse>>(mappedList); 
    }
}