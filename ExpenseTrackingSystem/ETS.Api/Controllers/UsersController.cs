using System.Security.Claims;
using ETS.Base.Response;
using ETS.Business.CQRS;
using ETS.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator mediator;

    public UsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet("MyProfile")]
    [Authorize(Roles = "admin, personal")]
    public async Task<ApiResponse<UserResponse>> MyProfile()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetUserByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<UserResponse>>> Get()
    {
        var operation = new GetAllUsersQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<UserResponse>> Get(int id)
    {
        var operation = new GetUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? UserName)
    {
        var operation = new GetUserByParameterQuery(FirstName,LastName,UserName);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest User)
    {
        var operation = new CreateUserCommand(User);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UserRequest User)
    {
        var operation = new UpdateUserCommand(id,User );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}