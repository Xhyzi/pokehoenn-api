using Microsoft.AspNetCore.Mvc;
using Pokehoenn.Api.Services;
using Pokehoenn.Api.Models;
using Pokehoenn.Api.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;
using Pokehoenn.Api.Mapping;
using System.Text.RegularExpressions;

namespace Pokehoenn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovesController(IMongoDbService db) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllMovesAsync()
        {
            var moves = await db.Moves.FindAsync(new BsonDocument()).Result.ToListAsync();

            return Ok(moves.Select(m => m.ToDto()).ToList());
        }

        [HttpGet("{name}", Name = "GetMove")]
        public async Task<IActionResult> GetMoveAsync(string name)
        {
            // non case-sensitive search of move name
            FilterDefinition<Move> filter = Builders<Move>.Filter
                                                          .Regex("name", 
                                                            new BsonRegularExpression($"^{Regex.Escape(name)}", "i"));
            var move = await db.Moves
                               .FindAsync(filter)
                               .Result
                               .FirstOrDefaultAsync();
            /*if (move == null)
                return NotFound();*/

            return Ok(move.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoveAsync([FromBody] MoveDto dto)
        {
            if (dto == null)
                return BadRequest();

            // non case-sensitive search of move name
            FilterDefinition<Move> filter = Builders<Move>.Filter
                                                          .Regex("name",
                                                          new BsonRegularExpression($"^{Regex.Escape(dto.Name)}", "i"));
            var existingMove = await db.Moves
                                       .FindAsync(filter)
                                       .Result
                                       .FirstOrDefaultAsync();
            if (existingMove != null)
            {
                ModelState.AddModelError("DuplicateMove", $"A move with name: {dto.Name} already exists.");
                return Conflict(ModelState);
            }

            await db.Moves.InsertOneAsync(dto.ToModel());
            
            return CreatedAtRoute("GetMove", new { name = dto.Name}, dto);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateMoveAsync(string name, [FromBody] UpdateMoveDto dto)
        {
            if (dto == null)
                return BadRequest();

            // non case-sensitive search of move name
            FilterDefinition<Move> filter = Builders<Move>.Filter
                                                          .Regex("name",
                                                          new BsonRegularExpression($"^{Regex.Escape(name)}", "i"));
            var existingMove = await db.Moves
                                       .FindAsync(filter)
                                       .Result
                                       .FirstOrDefaultAsync();
            if (existingMove == null)
                return NotFound();

            var move = dto.ToModel(existingMove.Id!, name);
            var update = await db.Moves.ReplaceOneAsync(m => m.Id == move.Id, move);

            if (update.IsAcknowledged)
                return NoContent();
            return StatusCode(500, $"An error has occurred while trying to update move with name: {name}");
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteMove(string name)
        {
            // non case-sensitive search of move name
            FilterDefinition<Move> filter = Builders<Move>.Filter
                                                          .Regex("name",
                                                          new BsonRegularExpression($"^{Regex.Escape(name)}", "i"));
            var existingMove = await db.Moves
                                       .FindAsync(filter)
                                       .Result
                                       .FirstOrDefaultAsync();
            if (existingMove == null) 
                return NotFound();

            await db.Moves.DeleteOneAsync(m => m.Name == name);
            return NoContent();
        }

    }
}
