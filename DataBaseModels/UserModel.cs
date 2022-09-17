using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DataBaseModles;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}