using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataBaseModles;
using DataBaseModles.ViewModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class userRepo : IUserRepo
{
    private readonly IMongoCollection<UserModel> _UserCollection;
    private  IHashHelper _HashHelper;
    private IJwtHelper _jwtHelper;
    private IConfiguration _config;
    private IEmailHelper _emailHelper;

    public userRepo(IMongoClient client, IHashHelper hashHelper , IJwtHelper jwtHelper , IConfiguration config, IEmailHelper emailHelper)
    {
        var database = client.GetDatabase("Voories");
        _UserCollection = database.GetCollection<UserModel>("Users");
        _HashHelper = hashHelper;
        _jwtHelper = jwtHelper;
        _config = config;
        _emailHelper = emailHelper;
    }


    public bool CreateUser(UserView user)
    {
        var users = _UserCollection.Find(o => o.Email == user.Email).ToList();
        if (users.Count == 0)
        {
            var newUser = new UserModel()
            {
                UserId = ObjectId.GenerateNewId().ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.RoleName
            };
            _UserCollection.InsertOne(newUser);
            string token = VerifyToken(user.Email);
            return  _emailHelper.sendResetPassword(user.Email, token, _config.GetValue<string>("DeployUriFrontend"));
            
        }

        return false;

    }



    public dynamic GetGetUsers()
    {
        var users = _UserCollection.Find(_ => true).ToList();
        if (users.Count > 0)
        {
            return users;
        }

        return false;
    }
    
    
    private dynamic VerifyToken(string email)
    {
        var claims = new List<Claim>();
        var token = _jwtHelper.GetResetAndVerifyToken(email, TimeSpan.FromMinutes(30), claims.ToArray());
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}