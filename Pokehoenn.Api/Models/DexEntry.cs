using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pokehoenn.Api.Models
{
    public class DexEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("species")]
        public required string Species { get; set; }

        [BsonElement("national_id")]
        public required int NationalId { get; set; }

        [BsonElement("regional_id")]
        public int RegionalId { get; set; }

        [BsonElement("category_name")]
        public string CategoryName { get; set; }

        [BsonElement("height")]
        public decimal Height { get; set; }

        [BsonElement("weight")]
        public decimal Weight { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
