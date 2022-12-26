using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles;

public class EmailModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdEmail  { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public bool Staatmaker { get; set; }
    
}