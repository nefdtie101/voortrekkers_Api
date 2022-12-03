using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseModles;

public class ModuleModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdModule { get; set; }
    
    public string ModuleName { get; set; }
    
    public string ModuleUri { get; set; }
}