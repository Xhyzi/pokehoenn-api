namespace Pokehoenn.Api.Services
{
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; } = null!;

        public string DbName { get; set; } = null!;

        public string GrowthRateCollectionName { get; set; } = null!;
    }
}
