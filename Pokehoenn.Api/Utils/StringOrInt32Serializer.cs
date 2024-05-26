
namespace Pokehoenn.Api.Utils
{

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;
    using System;

    public class StringOrInt32Serializer : SerializerBase<string>
    {
        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;

            switch (bsonReader.CurrentBsonType)
            {
                case BsonType.String:
                    return bsonReader.ReadString();
                case BsonType.Int32:
                    return bsonReader.ReadInt32().ToString();
                default:
                    throw new BsonSerializationException($"Cannot deserialize BsonType {bsonReader.CurrentBsonType} to string");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
        {
            var bsonWriter = context.Writer;
            bsonWriter.WriteString(value);
        }
    }

}
