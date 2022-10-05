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
                var tokens = createTokens(loginModel.email);
                var filter = Builders<UserModel>.Filter.Eq("UserId", user[0].UserId);
                var update = Builders<UserModel>.Update
                    .Set("Token", tokens.refresh );
                _UserCollection.UpdateOne(filter, update);
                return tokens;
            }
            else
            {
                return new JwtModel()
                {
                    valid = false
                };
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
                string token = VerifyToken(loginModel.email);
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
                var filter = Builders<UserModel>.Filter.Eq("Email", user[0].Email);
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

    public JwtModel refreshJwtToken(string token , string email)
    {
        var user = _UserCollection.Find(u =>
            u.Email == email
        ).ToList();
        if (user[0].Token == token)
        {
            var tokens = createTokens(email);
            var filter = Builders<UserModel>.Filter.Eq("UserId", user[0].UserId);
            var update = Builders<UserModel>.Update
                .Set("Token", tokens.refresh );
            _UserCollection.UpdateOne(filter, update);
            return tokens;
        }

        return new JwtModel()
        {
            valid = false
        };
    }

    private JwtModel createTokens(string email)
    {
        var claims = new List<Claim>();
        var token = new JwtSecurityTokenHandler().WriteToken(_jwtHelper.GetJwtToken(email, TimeSpan.FromSeconds(30), claims.ToArray()));
        var refreshToken = new  JwtSecurityTokenHandler().WriteToken(_jwtHelper.GetRefreshToken(TimeSpan.FromHours(5)));
        return new JwtModel()
        {
            token = token,
            refresh = refreshToken,
            valid = true
        };
    }
    
    private dynamic VerifyToken(string email)
    {
        var claims = new List<Claim>();
        var token = _jwtHelper.GetResetAndVerifyToken(email, TimeSpan.FromMinutes(8), claims.ToArray());
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}