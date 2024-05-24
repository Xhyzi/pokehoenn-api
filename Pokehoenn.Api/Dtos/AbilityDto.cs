using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    public record class AbilityDto
    (
        [Required] int AbilityId,
        [Required][MaxLength(50)] string Name,
        [Required][MaxLength(200)] string? Description
    );
}
