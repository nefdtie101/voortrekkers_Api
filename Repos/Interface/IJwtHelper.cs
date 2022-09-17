using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Repos.Interface;

public interface IJwtHelper
{
    public JwtSecurityToken GetJwtToken(
        string username,
        TimeSpan expiration,
        Claim[] additionalClaims = null);
}