using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles.Forms;

public class BasicForm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdBasicForm { get; set; }
    
    public string Name { get; set; }
    
    public string surname {get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string idOrganization { get; set; }
}