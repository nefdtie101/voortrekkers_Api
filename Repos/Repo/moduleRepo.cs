using DataBaseModles;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class moduleRepo :IModuleRepo 
{
    
    private readonly IMongoCollection<ModuleModel> _ModuleCollection;
    public moduleRepo(IMongoClient client )
    {
        var database = client.GetDatabase("Voories");
        _ModuleCollection = database.GetCollection<ModuleModel>("Module");
    }

    public dynamic GetAllModules()
    {
     var module =   _ModuleCollection.Find(_ => true).ToList();
     if (module != null)
     {
         return module;
     }
     return false;
     
    }
    
    
    public bool CreateModule(ModuleModel module)
    {
        var users = _ModuleCollection.Find(o => o.ModuleUri == module.ModuleUri && o.ModuleName == module.ModuleName).ToList();
        if (users.Count == 0)
        {

            var newModule = new ModuleModel()
            {
                IdModule = ObjectId.GenerateNewId().ToString(),
                ModuleName = module.ModuleName,
                ModuleUri = module.ModuleUri
            };
            _ModuleCollection.InsertOne(newModule);
            return true;
        }
        return false;
    }
    
    public bool EditModule(ModuleModel module)
    {
        try
        {
            var filter = Builders<ModuleModel>.Filter.Eq("IdModule",  module.IdModule);
            var update = Builders<ModuleModel>.Update
                .Set("ModuleName", module.ModuleName)
                .Set("ModuleUri", module.ModuleUri);
            _ModuleCollection.UpdateOne(filter ,update);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
     public bool Delete(ModuleModel module)
        {
            try
            {
                var filter = Builders<ModuleModel>.Filter.Eq("IdModule",  module.IdModule);
                _ModuleCollection.DeleteOne(filter);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    
}