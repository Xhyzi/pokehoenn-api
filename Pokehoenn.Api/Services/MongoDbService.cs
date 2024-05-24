using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Services
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoDatabase _db;
        private readonly string _growthRateCollectionName;
        private readonly string _dexEntriesCollectionName;

        public IMongoCollection<GrowthRate> GrowthRates => _db.GetCollection<GrowthRate>(_growthRateCollectionName);
        public IMongoCollection<DexEntry> DexEntries => _db.GetCollection<DexEntry>(_dexEntriesCollectionName);

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            MongoClient client = new(settings.Value.ConnectionURI);
            _db = client.GetDatabase(settings.Value.DbName);
            _growthRateCollectionName = settings.Value.GrowthRateCollectionName;
            _dexEntriesCollectionName = settings.Value.DexEntryCollectionName;
        }
    }
}
