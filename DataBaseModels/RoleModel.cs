using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles;

public class RoleModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdRole { get; set; }
    
    public string RoleName { get; set;}
    
    public List<string> AllowedUrls { get; set; }
}