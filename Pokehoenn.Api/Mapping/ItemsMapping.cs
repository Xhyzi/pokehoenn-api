using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Mapping
{
    public static class ItemsMapping
    {
        public static ItemDto ToDto(this Item item)
        {
            return new ItemDto(
                item.ItemId,
                item.Name,
                item.Description!,
                item.Pocket!,
                item.Type!,
                item.Price,
                item.HoldEffect!,
                item.HoldEffectArg,
                item.BattleUsage!,
                item.Importance
            );
        }

        public static Item ToModel(this ItemDto dto)
        {
            return new Item
            {
                ItemId = dto.ItemId,
                Name = dto.Name,
                Description = dto.Description,
                Pocket = dto.Pocket,
                Type = dto.Type,
                Price = dto.Price,
                HoldEffect = dto.HoldEffect,
                HoldEffectArg = dto.HoldEffectArg,
                BattleUsage = dto.BattleUsage,
                Importance = dto.Importance
            };
        }

        public static Item ToModel(this UpdateItemDto dto, string id, int itemId)
        {
            return new Item
            {
                Id = id,
                ItemId = itemId,
                Name = dto.Name,
                Description = dto.Description,
                Pocket = dto.Pocket,
                Type = dto.Type,
                Price = dto.Price,
                HoldEffect = dto.HoldEffect,
                HoldEffectArg = dto.HoldEffectArg,
                BattleUsage = dto.BattleUsage,
                Importance = dto.Importance
            };
        }
    }
}
