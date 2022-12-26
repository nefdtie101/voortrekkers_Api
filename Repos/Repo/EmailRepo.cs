using DataBaseModles;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class EmailRepo : IEmailRepo
{
    private readonly IMongoCollection<EmailModel> _EmailCollection;

    public EmailRepo(IMongoClient client)
    {
        var database = client.GetDatabase("Voories");
        _EmailCollection = database.GetCollection<EmailModel>("Email");
    }


    public bool AddToEmailList(EmailModel email)
    {
        try
        {
            var emailList = _EmailCollection.Find(o => o.Email == email.Email ).ToList();
            if (emailList.Count == 0)
            {
                _EmailCollection.InsertOne(email);
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool MarkAsStaatmker(string email)
    {
        try
        {
            var filter = Builders<EmailModel>.Filter.Eq("Email", email);
            var update = Builders<EmailModel>.Update
                .Set("Staatmaker", true);
            _EmailCollection.UpdateOne(filter, update);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

   

    public dynamic GetAllEmailAddresses( bool isStaatmaker)
    {
        try
        {
            if (isStaatmaker)
            {
                return  _EmailCollection.Find(o => o.Staatmaker == isStaatmaker ).ToList();
            }
            else
            {
                return  _EmailCollection.Find(_ => true ).ToList();
            }
             
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool UnSubscribeEmail(string Email)
    {
        try
        {
            var filter = Builders<EmailModel>.Filter.Eq("Email", Email);
            _EmailCollection.DeleteOne(filter);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    
}