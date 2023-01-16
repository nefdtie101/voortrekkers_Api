using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles;

public class EventModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdEvent { get; set; }
    
    public EventType eventType { get; set; }
    
    public string EventName { get; set; }
    
    public string EventDiscription { get; set; }
    
    public DateTime EventDate { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string idOrganization { get; set; }
    
    public string RedirectUri { get; set; }
    
    public string Message { get; set; }
}