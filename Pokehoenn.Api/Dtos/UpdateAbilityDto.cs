using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    public record class UpdateAbilityDto
    (
        [Required][MaxLength(50)] string Name,
        [Required][MaxLength(200)] string? Description
    );
}
