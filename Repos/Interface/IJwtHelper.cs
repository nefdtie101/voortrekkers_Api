using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Repos.Interface;

public interface IJwtHelper
{
    public JwtSecurityToken GetJwtToken(
        string username,
        TimeSpan expiration,
        Claim[] additionalClaims = null);

    public JwtSecurityToken GetResetAndVerifyToken(
        string username,
        TimeSpan expiration,
        Claim[] additionalClaims = null
    );

    public string getJwtFromHttpHeader(HttpContext http);
    
}