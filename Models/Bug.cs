using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CDO.Models
{
    public class Bug
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}