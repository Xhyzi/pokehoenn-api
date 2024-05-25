using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Pokehoenn.Api.Utils;

namespace Pokehoenn.Api.Models
{
    public class Move
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("id")]
        public int MoveId { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("description")]
        public required string  Description { get; set; }

        [BsonElement("type")]
        public required string Type { get; set; }

        [BsonElement("power")]
        public required int Power { get; set; }

        [BsonElement("accuracy")]
        public required int Accuracy { get; set; }

        [BsonElement("pp")]
        public required int Pp { get; set; }

        [BsonElement("priority")]
        public required int Priority { get; set; }

        [BsonElement("split")]
        public required string Split { get; set; }

        [BsonElement("target")]
        public required List<string> Targets { get; set; }

        [BsonElement("effect")]
        public required string Effect {  get; set; }

        [BsonElement("effect_chance")]
        public required int EffectChance {  get; set; }

        [BsonElement("flags")]
        [BsonSerializer(typeof(StringOrInt32ListSerializer))]
        public required List<string> Flags {  get; set; }
    }
}
