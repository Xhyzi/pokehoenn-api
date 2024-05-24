using MongoDB.Bson.Serialization.Attributes;

namespace Pokehoenn.Api.Models
{
    public class Ability
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonElement("id")]
        public required int AbilityId { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }
    }
}
