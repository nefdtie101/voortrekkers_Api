using DataBaseModles;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class RoleRepo : IRoleRepo
{
    private readonly IMongoCollection<RoleModel> _RoleCollection;

    public RoleRepo(IMongoClient client)
    {
        var database = client.GetDatabase("Voories");
        _RoleCollection = database.GetCollection<RoleModel>("Roles");
    }
    
    public dynamic GetAllRoles()
    {
        var roles =  _RoleCollection.Find(_ => true).ToList();
        if (roles != null)
        {
            return roles;
        }
        return false;
    }
    
    public bool CreateRole(RoleModel role)
    {
        var users = _RoleCollection.Find(o => o.RoleName == role.RoleName).ToList();
        if (users.Count == 0)
        {
            role.IdRole = ObjectId.GenerateNewId().ToString();
            _RoleCollection.InsertOne(role);
            return true;
        }
        return false;
    }
    
    public bool EditRole(RoleModel role)
    {
        try
        {
            var filter = Builders<RoleModel>.Filter.Eq("IdRole",  role.IdRole);
            var update = Builders<RoleModel>.Update
                .Set("RoleName", role.RoleName)
                .Set("AllowedUrls", role.AllowedUrls);
            _RoleCollection.UpdateOne(filter ,update);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public bool Delete(RoleModel role)
    {
        try
        {
            var filter = Builders<RoleModel>.Filter.Eq("IdRole",  role.IdRole);
            _RoleCollection.DeleteOne(filter);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    
}