using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CDO.Models
{
    public class Token
    {
        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("TokenAccess")]
        public string TokenAccess { get; set; }
    }
}