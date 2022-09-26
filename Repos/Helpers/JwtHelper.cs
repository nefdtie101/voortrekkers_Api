using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repos.Interface;

namespace Repos.Helpers;

public class JwtHelper : IJwtHelper
{
    private readonly IConfiguration _config;
    
    public JwtHelper(IConfiguration configuration)
    {
        _config = configuration;
    }
    
    public JwtSecurityToken GetJwtToken(
        string username,
        TimeSpan expiration,
        Claim[] additionalClaims = null)
    {
           
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email,username),
        };

        if (additionalClaims is object)
        {
            var claimList = new List<Claim>(claims);
            claimList.AddRange(additionalClaims);
            claims = claimList.ToArray();
        }
           
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _config.GetValue<string>("JWT:Key")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(expiration),
            claims: claims,
            signingCredentials: creds
        );
    }

    #region ResetVerify
    public JwtSecurityToken GetResetAndVerifyToken(
        string username,
        TimeSpan expiration,
        Claim[] additionalClaims = null
    )
    {
        var claims = new[]
        {
            new Claim("Email",username),
        };
        
        if (additionalClaims is object)
        {
            var claimList = new List<Claim>(claims);
            claimList.AddRange(additionalClaims);
            claims = claimList.ToArray();
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _config.GetValue<string>("JWT:VerifyToken")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(expiration),
            claims: claims,
            signingCredentials: creds
        );
    }
    
    #endregion
   

    public string getJwtFromHttpHeader(HttpContext http)
    {
        try
        {
            var accessToken = http.Request.Headers.Authorization.ToString(); 
            var array = accessToken.Split(' ');
            var token = array[1];
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "";
        }
    }
    
}