using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles.Forms;

public class BasicForm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdBasicForm { get; set; }
    
    public string Name { get; set; }
    
    public string Surname {get; set; }
    
    public string EMail { get; set; }
    
    public bool Paid { get; set; }
    
    public bool Attended  { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string idOrganization { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdEvent { get; set; }
}