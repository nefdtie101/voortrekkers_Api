using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataBaseModles;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class LoginRepo : ILoginRepo
{
    private readonly IMongoCollection<UserModel> _UserCollection;
    private readonly IHashHelper _HashHelper;
    private readonly IJwtHelper _jwtHelper;
    private readonly IEmailHelper _emailHelper;

    public LoginRepo(IMongoClient client , IHashHelper hashHelper, IJwtHelper jwtHelper, IEmailHelper emailHelper)
    {
        var database = client.GetDatabase("Voories");
        _UserCollection = database.GetCollection<UserModel>("Users");
        _HashHelper = hashHelper;
        _jwtHelper = jwtHelper;
        _emailHelper = emailHelper;
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

    public bool resetPassword()
    {
     return  _emailHelper.sendResetPassword("johann@nefdtco.co.za", "rewrirrrr", "google.com");
    }

    private dynamic createTokens(LoginModel user)
    {
        var claims = new List<Claim>();
        var token = _jwtHelper.GetJwtToken(user.email, TimeSpan.FromMinutes(10), claims.ToArray());
        return new JwtSecurityTokenHandler().WriteToken(token);;
    }
    
    
    
}