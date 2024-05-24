using Microsoft.AspNetCore.Mvc;
using Pokehoenn.Api.Services;
using Pokehoenn.Api.Mapping;
using Pokehoenn.Api.Models;
using Pokehoenn.Api.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Pokehoenn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbilitiesController(IMongoDbService db) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAbilitiesAsync()
        {

            var abilities = await db.Abilities
                                    .FindAsync(new BsonDocument())
                                    .Result
                                    .ToListAsync();

            return Ok(abilities.Select(a => a.ToDto()).ToList());
        }

        [HttpGet("{abilityId:int}", Name = "GetAbility")]
        public async Task<IActionResult> GetAbilityByIdAsync(int abilityId)
        {
            var ability = await db.Abilities
                                  .FindAsync(a => a.AbilityId == abilityId)
                                  .Result
                                  .FirstOrDefaultAsync();

            if (ability == null)
            {
                return NotFound();
            }
            return Ok(ability.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbilityAsync([FromBody] AbilityDto abilityDto)
        {
            if (abilityDto == null)
                return BadRequest();

            // checks if ability Id is already used on db
            var existingAbility = await db.Abilities
                                          .Find(a => a.AbilityId == abilityDto.AbilityId)
                                          .FirstOrDefaultAsync();
            if (existingAbility != null)
            {
                ModelState.AddModelError("DuplicateAbility", 
                    $"An ability with ability id {abilityDto.AbilityId} already exists.");
                return Conflict(ModelState);
            }

            await db.Abilities.InsertOneAsync(abilityDto.ToModel());

            return CreatedAtRoute("GetAbility", new { id = abilityDto.AbilityId }, abilityDto);
        }

        [HttpPut("{abilityId:int}")]
        public async Task<IActionResult> UpdateAbilityAsync(int abilityId, [FromBody] UpdateAbilityDto abilityDto)
        {
            // no payload has been sent
            if (abilityDto == null)
                return BadRequest();

            // checks if the resource to be updated exists
            var existingAbility = await db.Abilities
                                          .FindAsync(a => a.AbilityId == abilityId)
                                          .Result
                                          .FirstOrDefaultAsync();
            if (existingAbility == null)
                return NotFound();

            // tries to update the data
            var ability = abilityDto.ToModel(existingAbility.Id!, abilityId);
            var updated = await db.Abilities.ReplaceOneAsync(a => a.AbilityId == abilityId, ability);

            if (updated.IsAcknowledged /*&& updated.ModifiedCount > 0*/)
                return NoContent();

            return StatusCode(500, new { Message = "An error occurred while updating the data." });
        }

        [HttpDelete("{abilityId:int}")]
        public async Task<IActionResult> DeleteAbilityAsync(int abilityId)
        {
            // checks if given ability exists
            var existingAbility = await db.Abilities
                                          .FindAsync(a => a.AbilityId == abilityId)
                                          .Result
                                          .FirstOrDefaultAsync();
            if (existingAbility == null)
                return NotFound();

            // tries to delete the ability
            await db.Abilities.DeleteOneAsync(a => a.AbilityId == abilityId);
            return NoContent();
        }
    }
}
