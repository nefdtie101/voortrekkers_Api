using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles.Forms;

public class BasicStudentForm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdBasicStudentForm { get; set; }
    
    public string Name { get; set; }
    
    public string surname {get; set; }
    
    public string Koemandoe { get; set; }
    
    public string Graad { get; set; }
}