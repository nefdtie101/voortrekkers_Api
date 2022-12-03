using DataBaseModles;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class EventRepo: IEventRepo
{
    private readonly IMongoCollection<EventModel> _EventCollection;
    public EventRepo(IMongoClient client)
    {
        var database = client.GetDatabase("Voories");
        _EventCollection = database.GetCollection<EventModel>("Events");
    }
    
    public dynamic GetAllEvents()
    {
        var events = _EventCollection.Find(_ => true).ToList();
        if (events != null)
        {
            return events;
        }

        return false;
    }
    
    public bool CreateEvents(EventModel eModel)
    {
        var events = _EventCollection.Find(o => o.EventName == eModel.EventName).ToList();
        if (events.Count == 0)
        {

            var newEevnt = new EventModel()
            {
                IdEvent = ObjectId.GenerateNewId().ToString(),
                eventType = eModel.eventType,
                EventName = eModel.EventName,
                EventDiscription = eModel.EventDiscription,
                EventDate = eModel.EventDate,
                idOrganization = eModel.idOrganization,
                RedirectUri = eModel.RedirectUri
            };
            _EventCollection.InsertOne(newEevnt);
            return true;
        }
        return false;
    }
    
    public bool EditEvent(EventModel eModel)
    {
        try
        {
            var filter = Builders<EventModel>.Filter.Eq("IdEvent",  eModel.IdEvent);
            var update = Builders<EventModel>.Update
                .Set("eventType", eModel.eventType)
                .Set("EventName", eModel.EventName)
                .Set("EventDiscription", eModel.EventDiscription)
                .Set("EventDate", eModel.EventDate)
                .Set("RedirectUri", eModel.RedirectUri);
                
            _EventCollection.UpdateOne(filter ,update);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public bool Delete(EventModel eModel)
    {
        try
        {
            var filter = Builders<EventModel>.Filter.Eq("IdEvent", eModel.IdEvent);
            _EventCollection.DeleteOne(filter);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

}