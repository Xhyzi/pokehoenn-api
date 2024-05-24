using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Mapping;
using Pokehoenn.Api.Services;

namespace Pokehoenn.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IMongoDbService db) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            var items = await db.Items.FindAsync(new BsonDocument()).Result.ToListAsync();
            
            return Ok(items.Select(i => i.ToDto()).ToList());
        }

        [HttpGet("{itemId:int}", Name = "GetItem")]
        public async Task<IActionResult> GetItemByIdAsync(int itemId)
        {
            var item = await db.Items
                               .FindAsync(i => i.ItemId == itemId)
                               .Result
                               .FirstOrDefaultAsync();
            if (item == null)
                return NotFound();

            return Ok(item.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemAsync([FromBody] ItemDto dto)
        {
            // checks if dto is null
            if (dto == null)
                return BadRequest();

            // check if itemId is already in use
            var existingItem = await db.Items
                                       .FindAsync(i => i.ItemId == dto.ItemId)
                                       .Result
                                       .FirstOrDefaultAsync();
            if (existingItem != null)
            {
                ModelState.AddModelError("DuplicateItem", $"Item with itemId: {dto.ItemId} already exists");
                return Conflict(ModelState);
            }

            await db.Items.InsertOneAsync(dto.ToModel());

            return CreatedAtRoute("GetItem", new { itemId = dto.ItemId }, dto );
        }

        [HttpPut("{itemId:int}")]
        public async Task<IActionResult> UpdateItemAsync(int itemId, [FromBody] UpdateItemDto dto)
        {
            // checks if item is null
            if (dto == null)
                return BadRequest();

            // check if itemId exists in database
            var existingItem = await db.Items
                                       .FindAsync(i => i.ItemId == itemId)
                                       .Result
                                       .FirstOrDefaultAsync();
            if (existingItem == null)
                return NotFound();

            var item = dto.ToModel(existingItem.Id!, itemId);
            var update = await db.Items.ReplaceOneAsync(i => i.ItemId == itemId, item);

            if (update.IsAcknowledged)
                return NoContent();

            return StatusCode(500, new { Message = "An error occurred while updating the data" });
        }

        [HttpDelete("{itemId:int}")]
        public async Task<IActionResult> DeleteItemAsync(int itemId)
        {
            // check if item exists in db
            var existingItem = await db.Items
                                       .FindAsync(i => i.ItemId == itemId)
                                       .Result
                                       .FirstOrDefaultAsync();
            if (existingItem == null)
                return NotFound();

            // deletes item
            await db.Items.DeleteOneAsync(i => i.ItemId == itemId);
            return NoContent();
        }
    }
}
