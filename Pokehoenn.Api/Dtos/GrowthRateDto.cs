using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    public record class GrowthRateDto
    (
        [Required][MaxLength(50)] string Name,
        [Required][MaxLength(300)] string Formula,
        [Range(1, 100)] int MaxExp
    );
}
