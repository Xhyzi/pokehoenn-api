using MongoDB.Driver;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Services
{
    public interface IMongoDbService
    {
        public IMongoCollection<GrowthRate> GrowthRates { get; }
    }
}
