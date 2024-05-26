using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Pokehoenn.Api.Utils;

namespace Pokehoenn.Api.Models
{
    public class Specie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("dex_num")]
        public int DexNum { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("types")]
        public List<string> Types { get; set; }

        [BsonElement("base_hp")]
        public int BaseHp { get; set; }

        [BsonElement("base_atk")]
        public int BaseAtk { get; set; }

        [BsonElement("base_def")]
        public int BaseDef { get; set; }

        [BsonElement("base_spd")]
        public int BaseSpd { get; set; }

        [BsonElement("base_sp_atk")]
        public int BaseSpAtk { get; set; }

        [BsonElement("base_sp_def")]
        public int BaseSpDef { get; set; }

        [BsonElement("abilities")]
        public List<string> Abilities { get; set; }

        [BsonElement("hidden_ability")]
        [BsonSerializer(typeof(StringOrInt32Serializer))]
        public string HiddenAbility { get; set; }

        [BsonElement("catch_rate")]
        public int CatchRate { get; set; }

        [BsonElement("growth_rate")]
        public string GrowthRate { get; set; }

        [BsonElement("exp_yield")]
        public int ExpYield { get; set; }

        [BsonElement("ev_yield")]
        public List<List<object>> EvYield { get; set; }

        [BsonElement("female_ratio")]
        public double FemaleRatio { get; set; }

        [BsonElement("egg_cycles")]
        public int EggCycles { get; set; }

        [BsonElement("egg_groups")]
        public List<string> EggGroups { get; set; }

        [BsonElement("friendship")]
        public int Friendship { get; set; }

        [BsonElement("body_color")]
        public string BodyColor { get; set; }

        [BsonElement("items")]
        [BsonSerializer(typeof(StringOrInt32ListSerializer))]
        public List<string> Items { get; set; }

        [BsonElement("safari_flee_rate")]
        public int SafariFleeRate { get; set; }

        [BsonElement("flags")]
        public object Flags { get; set; }
    }
}
