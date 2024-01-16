using ETS.Base.Response;
using ETS.Business.CQRS;
using ETS.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokensController : ControllerBase
{
    private readonly IMediator mediator;

    public TokensController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        var operation = new CreateTokenCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}