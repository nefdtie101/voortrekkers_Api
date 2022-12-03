using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles;

public class Organization
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string idOrganization { get; set; }
    
    public string name { get; set; }
    
    public string Discription { get; set; }


}