using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Pokehoenn.Api.Services;
using MongoDB.Driver;
using Pokehoenn.Api.Mapping;
using Pokehoenn.Api.Models;
using System.Text.RegularExpressions;
using Pokehoenn.Api.Dtos;
using System.Xml.Linq;

namespace Pokehoenn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController(IMongoDbService db) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSpecies()
        {
            var species = await db.Species.FindAsync(new BsonDocument()).Result.ToListAsync();

            return Ok(species.Select(s => s.ToDto()).ToList());
        }

        [HttpGet("{name}", Name = "GetSpecie")]
        public async Task<IActionResult> GetSpecie(string name)
        {
            FilterDefinition<Specie> filter = Builders<Specie>.Filter
                                                              .Regex("name",
                                                                new BsonRegularExpression($"^{Regex.Escape(name)}", "i"));
            var specie = await db.Species.FindAsync(filter)
                                         .Result
                                         .FirstOrDefaultAsync();
            if (specie == null)
                return NotFound();

            return Ok(specie.ToDto());
        }
    }
}
