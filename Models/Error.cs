using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CDO.Models
{
    public class Error
    {
        [BsonElement("StatusError")]
        public int StatusError { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}