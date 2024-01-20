using System.Security.Claims;
using ETS.Base.Response;
using ETS.Business.CQRS;
using ETS.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETS.Api.Controllers;

[Route("[controller]")]
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
    
    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetAllUsers()
    {
        var operation = new GetAllUsersQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetUserByParameter")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetUserByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? UserName)
    {
        var operation = new GetUserByParameterQuery(FirstName,LastName,UserName);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("CreateUser")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<UserResponse>> CreateUser([FromBody] UserRequest User)
    {
        var operation = new CreateUserCommand(User);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdateUser/{id}")]
    [Authorize(Roles = "admin, personal")]
    public async Task<ApiResponse> UpdateUser(int id, [FromBody] UserRequest User)
    {
        var operation = new UpdateUserCommand(id,User );
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("DeleteUser/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> DeleteUser(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}