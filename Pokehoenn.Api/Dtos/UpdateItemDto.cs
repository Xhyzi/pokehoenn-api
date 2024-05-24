using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    public record class UpdateItemDto
    (
        [Required][MaxLength(40)] string Name,
        [MaxLength(300)] string Description,
        [AllowedValues(
            "none",
            "items",
            "poke_balls",
            "tm_hm",
            "berries",
            "key_items")] string Pocket,
        [MaxLength(30)] string Type,
        [Range(0, 999999)] int Price,
        [MaxLength(50)] string HoldEffect,
        [Range(0, 255)] int HoldEffectArg,
        [MaxLength(30)] string BattleUsage,
        [Range(0, 2)] int Importance
    );
}
