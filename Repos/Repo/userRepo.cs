using DataBaseModles;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class userRepo : IUserRepo
{
    private readonly IMongoCollection<UserModel> _UserCollection;
    private  IHashHelper _HashHelper;

    public userRepo(IMongoClient client, IHashHelper hashHelper)
    {
        var database = client.GetDatabase("Voories");
        _UserCollection = database.GetCollection<UserModel>("Users");
        _HashHelper = hashHelper;
    }

    public bool CreateNewUser(UserModel user)
    {
        var databaseModel = _UserCollection.Find(x =>
            x.Email == user.Email 
        ).Count();
        
        if (databaseModel == 0)
        {
            user.UserId = ObjectId.GenerateNewId().ToString();
            user.Password = _HashHelper.ToSHA256(user.Password);
            _UserCollection.InsertOne(user);
            return true;
        } else
        {
            return false;
        }
    }
}