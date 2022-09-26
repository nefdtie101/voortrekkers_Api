using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataBaseModles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Repos.Interface;
using Voortrekkers.Pages.ResetPassword;

namespace Repos.Repo;

public class LoginRepo : ILoginRepo
{
    private readonly IMongoCollection<UserModel> _UserCollection;
    private readonly IHashHelper _HashHelper;
    private readonly IJwtHelper _jwtHelper;
    private readonly IEmailHelper _emailHelper;
    private readonly IConfiguration _config;


    public LoginRepo(IMongoClient client , IHashHelper hashHelper, IJwtHelper jwtHelper, IEmailHelper emailHelper , IConfiguration configuration)
    {
        var database = client.GetDatabase("Voories");
        _UserCollection = database.GetCollection<UserModel>("Users");
        _HashHelper = hashHelper;
        _jwtHelper = jwtHelper;
        _emailHelper = emailHelper;
        _config = configuration;
    }


    public dynamic Login(LoginModel loginModel)
    {
        try
        {
            var user = _UserCollection.Find(u =>
                u.Email == loginModel.email
            ).ToList();
            var password =  _HashHelper.ToSHA256(loginModel.password);
        
            if(user[0].Password == password)
            {
                return createTokens(loginModel);
            }
            else
            {
                return false;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool resetPassword(LoginModel loginModel)
    {
        try
        {
            var user = _UserCollection.Find(u =>
                u.Email == loginModel.email
            ).ToList();
            if (user.Count > 0)
            {
                string token = VerifyToken(loginModel);
                return  _emailHelper.sendResetPassword(user[0].Email, token, _config.GetValue<string>("DeployUriFrontend"));
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool CreatePassword(ResetPasswordModel resetPassword, HttpContext http )
    {
        try
        {
            var emialUser = http.User.FindFirst("Email").Value; 
            var user = _UserCollection.Find(o => o.Email == emialUser).ToList();
            if (user.Count > 0 && resetPassword.Password == resetPassword.RePassword)
            {
                var filter = Builders<UserModel>.Filter.Eq("_Email", user[0]);
                var update = Builders<UserModel>.Update
                    .Set("Password",  _HashHelper.ToSHA256(resetPassword.Password));
                _UserCollection.UpdateOne(filter,update);
            }
            else
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private dynamic createTokens(LoginModel user)
    {
        var claims = new List<Claim>();
        var token = _jwtHelper.GetJwtToken(user.email, TimeSpan.FromMinutes(10), claims.ToArray());
        return new JwtSecurityTokenHandler().WriteToken(token);;
    }
    
    private dynamic VerifyToken(LoginModel user)
    {
        var claims = new List<Claim>();
        var token = _jwtHelper.GetResetAndVerifyToken(user.email, TimeSpan.FromMinutes(8), claims.ToArray());
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}