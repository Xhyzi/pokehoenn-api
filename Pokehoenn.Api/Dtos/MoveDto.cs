using System.ComponentModel.DataAnnotations;

namespace Pokehoenn.Api.Dtos
{
    
    public record class MoveDto
    (
        [Required][MaxLength(20)] string Name,
        [MaxLength(200)] string Description,
        [Required]
        [AllowedValues(
            "Normal",
            "Lucha",
            "Volador",
            "Veneno",
            "Tierra",
            "Roca",
            "Bicho",
            "Fantasma",
            "Acero",
            "(?)",
            "Fuego",
            "Agua",
            "Planta",
            "Psíquico",
            "Hielo",
            "Dragón", 
            "Siniestro",
            "Hada")] 
        string Type,
        [Range(0, 300)] int Power,
        [Range(0, 100)] int Accuracy,
        [Range(5, 40)] int Pp,
        [Range(-10, 10)] int Priority,
        [Required]
        [AllowedValues(
            "physical",
            "special",
            "status")]
        string Split,
        [Required] List<string> Targets,
        [Required] string Effect,
        [Range(0, 100)] int EffectChance,
        [Required] List<string> Flags

    );
}
