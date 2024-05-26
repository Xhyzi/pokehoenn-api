using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    public record class SpecieDto
    (
        [Required][MaxLength(20)] string Name,
        [Required] int DexNum,
        [Required] List<string> Types,
        [Required][Range(0, 255)] int BaseHp,
        [Required][Range(0, 255)] int BaseAtk,
        [Required][Range(0, 255)] int BaseDef,
        [Required][Range(0, 255)] int BaseSpd,
        [Required][Range(0, 255)] int BaseSpAtk,
        [Required][Range(0, 255)] int BaseSpDef,
        [Required] List<string> Abilities,
        [Required] string HiddenAbility,
        [Required][Range(0, 255)] int CatchRate,
        [Required] string GrowthRate,
        [Required][Range(0, 255)] int ExpYield,
        [Required] List<List<object>> EvYield, // list that contains both int and string
        [Required][Range(0, 1)] double FemaleRatio,
        [Required] int EggCycles,
        [Required] List<string> EggGroups,
        [Required] int Friendship,
        [Required] string BodyColor,
        [Required] List<string> Items,
        [Required] [Range(0, 255)] int SafariFleeRate,
        [Required] object Flags // To handle both int and string
    );
}
