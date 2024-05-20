using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pokehoenn.Api.Models
{
    public class GrowthRate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // allows to work with id as a string
        public required string Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("formula")]
        public required string Formula { get; set; }

        [BsonElement("max_exp")]
        public int MaxExp {  get; set; }
    }
}
