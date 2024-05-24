using MongoDB.Driver;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Services
{
    public interface IMongoDbService
    {
        public IMongoCollection<GrowthRate> GrowthRates { get; }

        public IMongoCollection<DexEntry> DexEntries { get; }

        public IMongoCollection<Ability> Abilities { get; }
    }
}
