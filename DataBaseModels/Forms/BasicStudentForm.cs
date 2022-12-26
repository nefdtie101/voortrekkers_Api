using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles.Forms;

public class BasicStudentForm
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdBasicStudentForm { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdEvent { get; set; }
    
    public string Name { get; set; }
    
    public string Surname {get; set; }
    
    public string Koemandoe { get; set; }
    
    public string Graad { get; set; }
    
    public string mobileNumber { get; set; }
    
    public string EMail { get; set; }
    
    public bool Paid { get; set; }
    
    public bool Attended  { get; set; }
}