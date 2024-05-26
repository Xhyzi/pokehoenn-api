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
        private readonly string _abilitiesCollectionName;
        private readonly string _itemsCollectionName;
        private readonly string _movesCollectionName;
        private readonly string _specieCollectionName;

        public IMongoCollection<GrowthRate> GrowthRates => _db.GetCollection<GrowthRate>(_growthRateCollectionName);
        public IMongoCollection<DexEntry> DexEntries => _db.GetCollection<DexEntry>(_dexEntriesCollectionName);
        public IMongoCollection<Ability> Abilities => _db.GetCollection<Ability>(_abilitiesCollectionName);
        public IMongoCollection<Item> Items => _db.GetCollection<Item>(_itemsCollectionName);
        public IMongoCollection<Move> Moves => _db.GetCollection<Move>(_movesCollectionName);
        public IMongoCollection<Specie> Species => _db.GetCollection<Specie>(_specieCollectionName);

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            MongoClient client = new(settings.Value.ConnectionURI);
            _db = client.GetDatabase(settings.Value.DbName);
            _growthRateCollectionName = settings.Value.GrowthRateCollectionName;
            _dexEntriesCollectionName = settings.Value.DexEntryCollectionName;
            _abilitiesCollectionName = settings.Value.AbilitiesCollectionName;
            _itemsCollectionName = settings.Value.ItemsCollectionName;
            _movesCollectionName = settings.Value.MovesCollectionName;
            _specieCollectionName = settings.Value.SpecieCollectionName;
        }
    }
}
