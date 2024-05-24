using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    public record class DexEntryDto
    (
        [Required][MaxLength(20)] string Species,
        [Required] int NationalId,
        [Range(0, 202)] int RegionalId,
        [MaxLength(40)] string CategoryName,
        [Range(0.3, 100)] decimal Height,
        [Range(0.1, 100000)] decimal Weight,
        [MaxLength(500)] string Description
    );
}
