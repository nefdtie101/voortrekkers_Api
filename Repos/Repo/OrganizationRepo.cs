using DataBaseModles;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class OrganizationRepo : IOrganizationRepo
{
    private readonly IMongoCollection<Organization> _OrganizationCollection;
    public OrganizationRepo(IMongoClient client)
    {
        var database = client.GetDatabase("Voories");
        _OrganizationCollection = database.GetCollection<Organization>("Organization");
    }

    public dynamic GetAllOrganizations()
    {
        var organizations = _OrganizationCollection.Find(_ => true).ToList();
        if (organizations != null)
        {
            return organizations;
        }

        return false;
    }
    
    public bool CreateOrganization(Organization org)
    {
        var organization = _OrganizationCollection.Find(o => o.name == org.name).ToList();
        if (organization.Count == 0)
        {

            var newOrganization = new Organization()
            {
                idOrganization = ObjectId.GenerateNewId().ToString(),
                name = org.name,
                Discription = org.Discription
            };
            _OrganizationCollection.InsertOne(newOrganization);
            return true;
        }
        return false;
    }
    
    public bool EditOrganization(Organization org)
    {
        try
        {
            var filter = Builders<Organization>.Filter.Eq("idOrganization",  org.idOrganization);
            var update = Builders<Organization>.Update
                .Set("name", org.name)
                .Set("Discription", org.Discription);
            _OrganizationCollection.UpdateOne(filter ,update);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public bool Delete(Organization org)
    {
        try
        {
            var filter = Builders<Organization>.Filter.Eq("idOrganization", org.idOrganization);
            _OrganizationCollection.DeleteOne(filter);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }


}