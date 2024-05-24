using Microsoft.AspNetCore.Mvc;
using Pokehoenn.Api.Services;
using Pokehoenn.Api.Models;
using Pokehoenn.Api.Mapping;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using Pokehoenn.Api.Dtos;

namespace Pokehoenn.Api.Controllers
{
    /// <summary>
    /// API Controller for the DexEntries Endpoint
    /// </summary>
    /// <param name="db">Injects the MongoDb service</param>
    [Route("api/[controller]")]
    [ApiController]
    public class DexEntriesController(IMongoDbService db) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDexEntriesAsync()
        {
            var dexEntries = await db.DexEntries.FindAsync(new BsonDocument()).Result.ToListAsync();
            return Ok(dexEntries.Select(de => de.ToDto()).ToList());
        }

        [HttpGet("{species}", Name = "GetDexEntry")]
        public async Task<IActionResult> GetDexEntryByIdAsync(string species)
        {
            try
            {
                FilterDefinition<DexEntry> filter;
                if (int.TryParse(species, out int id)) // checks if species is an id
                {
                    filter = Builders<DexEntry>.Filter.Eq("national_id", id);
                }
                else // species is given as a string
                {
                    // filter is case-insensitive for species name
                    filter = Builders<DexEntry>.Filter.Regex("species", new BsonRegularExpression($"^{Regex.Escape(species)}", "i"));
                }
                DexEntry dexEntry = await db.DexEntries.FindAsync(filter).Result.FirstAsync();
                return Ok(dexEntry.ToDto());
            }
            catch (InvalidOperationException) // cannot find any entry that matches filter
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDexEntryAsync([FromBody] DexEntryDto entryDto)
        {
            try
            {
                // Entry is not null
                if (entryDto == null)
                    return BadRequest();
                // Entry is not empty
                if (string.IsNullOrWhiteSpace(entryDto.Species))
                    ModelState.AddModelError("Species", "The species name should not be empty");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Entry does already exist
                var existingEntry = await db.DexEntries.Find(e => e.Species == entryDto.Species || e.NationalId == entryDto.NationalId).FirstOrDefaultAsync();
                if (existingEntry != null)
                {
                    ModelState.AddModelError("DuplicateEntry", "An entry with the same species name or national id already exists.");
                    return Conflict(ModelState);
                }

                await db.DexEntries.InsertOneAsync(entryDto.ToModel());

                return CreatedAtRoute("GetDexEntry", new { species = entryDto.NationalId}, entryDto);
                //return Created("Created", true);
            }
            catch (InvalidOperationException) // cannot find any entry that matches filter
            {
                return NotFound();
            }
        }

        [HttpPut("{nationalId:int}")]
        public async Task<IActionResult> UpdateDexEntryAsync(int nationalId, [FromBody] DexEntryDto entryDto)
        {
            try
            {
                if (entryDto == null)
                    return BadRequest();
                if (string.IsNullOrWhiteSpace(entryDto.Species))
                    ModelState.AddModelError("Species", "The species name should not be empty.");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // finds current entry and obtains its BsonId
                var existingEntry = await db.DexEntries.FindAsync(e => e.NationalId == nationalId).Result.FirstAsync();
                var newEntry = entryDto.ToModel();
                newEntry.Id = existingEntry.Id;

                // Checks if new entry collides with any other entry different than itself
                List<DexEntry> collidedEntries = await db.DexEntries
                                                         .Find(e => e.NationalId == newEntry.NationalId || e.Species == newEntry.Species)
                                                         .ToListAsync();
                foreach (DexEntry c in collidedEntries)
                    if (c.Id !=  newEntry.Id)
                    {
                        ModelState.AddModelError("DuplicateEntry", "An entry with the same species name or national id already exists.");
                        return Conflict(ModelState);
                    }

                // updates the data
                var updated = await db.DexEntries.ReplaceOneAsync(e => e.NationalId == nationalId, newEntry);

                if (updated.IsAcknowledged && updated.ModifiedCount > 0)
                    return NoContent();

                return StatusCode(500, new { Message = "An error occurred while updating the data." });
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{species}")]
        public async Task<IActionResult> DeleteDexEntryAsync(string species)
        {
            FilterDefinition<DexEntry> filter;

            if (int.TryParse(species, out int nationalId))
            {
                filter = Builders<DexEntry>.Filter.Eq("national_id", nationalId);
            }
            else
            {
                filter = Builders<DexEntry>.Filter.Eq("species", species);
            }
            
            var existingEntry = db.DexEntries.FindAsync(filter).Result.FirstOrDefault();
            if (existingEntry == null)
                return NotFound();

            await db.DexEntries.DeleteOneAsync(filter);
            return NoContent();
        }
    }
}
