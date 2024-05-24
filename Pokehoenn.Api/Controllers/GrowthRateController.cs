using Microsoft.AspNetCore.Mvc;
using Pokehoenn.Api.Services;
using Pokehoenn.Api.Models;
using Pokehoenn.Api.Mapping;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Pokehoenn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrowthRateController(IMongoDbService db) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetAllGrowthRatesAsync()
        {
            var growthRates = await db.GrowthRates.FindAsync(new BsonDocument()).Result.ToListAsync();
            return Ok(growthRates.Select(gr => gr.ToDto()).ToList());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetGrowthRate(string name)
        {
            try
            {
                FilterDefinition<GrowthRate> filter = Builders<GrowthRate>.Filter.Eq("name", name);
                GrowthRate growthRate = await db.GrowthRates.FindAsync<GrowthRate>(filter).Result.FirstAsync();
                return Ok(growthRate.ToDto());

            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
