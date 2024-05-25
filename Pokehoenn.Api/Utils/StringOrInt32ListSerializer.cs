using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Pokehoenn.Api.Utils
{
    /// <summary>
    /// Used for the serialization and deserialization of JSON fields that can contain a list
    /// of both string and integers
    /// </summary>
    public class StringOrInt32ListSerializer : SerializerBase<List<string>>
    {
        public override List<string> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var list = new List<string>();

            var currentBsonType = bsonReader.GetCurrentBsonType();

            if (currentBsonType == BsonType.Array)
            {
                bsonReader.ReadStartArray();

                while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
                {
                    switch (bsonReader.CurrentBsonType)
                    {
                        case BsonType.String:
                            list.Add(bsonReader.ReadString());
                            break;
                        case BsonType.Int32:
                            list.Add(bsonReader.ReadInt32().ToString());
                            break;
                        default:
                            throw new BsonSerializationException($"Cannot deserialize BsonType {bsonReader.CurrentBsonType} to List<string>");
                    }
                }

                bsonReader.ReadEndArray();
            }
            else if (currentBsonType == BsonType.Int32)
            {
                list.Add(bsonReader.ReadInt32().ToString());
            }
            else if (currentBsonType == BsonType.String)
            {
                list.Add(bsonReader.ReadString());
            }
            else
            {
                throw new BsonSerializationException($"Unexpected BsonType {currentBsonType}");
            }

            return list;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, List<string> value)
        {
            var bsonWriter = context.Writer;

            bsonWriter.WriteStartArray();

            foreach (var item in value)
                bsonWriter.WriteString(item);

            bsonWriter.WriteEndArray();
        }
    }
}
