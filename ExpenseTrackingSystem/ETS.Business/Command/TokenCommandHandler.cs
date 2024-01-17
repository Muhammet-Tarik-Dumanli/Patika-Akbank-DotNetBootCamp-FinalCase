using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ETS.Base.Encryption;
using ETS.Base.Response;
using ETS.Base.Token;
using ETS.Business.CQRS;
using ETS.Data;
using ETS.Data.Entity;
using ETS.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ETS.Business.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly ETSDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenCommandHandler(ETSDbContext dbContext,IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }
    
    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        string hash = Md5Extension.GetHash(request.Model.Password.Trim());
        if (hash != user.Password)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        string token = Token(user);

        return new ApiResponse<TokenResponse>( new TokenResponse()
        {
            Email = user.Email,
            Token = token,
            ExpireDate =  DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
        });
    }
    
    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("UserName", user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        return claims;
    }
}