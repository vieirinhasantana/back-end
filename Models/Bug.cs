using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CDO.Models
{
    public class Bug
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Severity")]
        public string Severity { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Product")]
        public string Product { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; }
    }
}