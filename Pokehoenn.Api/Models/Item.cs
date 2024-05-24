using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Pokehoenn.Api.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("item_id")]
        public required int ItemId { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("pocket")]
        public string? Pocket { get; set; }

        [BsonElement("type")]
        public string? Type { get; set; }

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("hold_effect")]
        public string? HoldEffect { get; set; }

        [BsonElement("hold_effect_arg")]
        public int HoldEffectArg { get; set; }

        [BsonElement("battle_usage")]
        public string? BattleUsage { get; set; }

        [BsonElement("secondary_id")]
        public int SecondaryId { get; set; }

        [BsonElement("importance")]
        public int Importance { get; set; }
    }
}
