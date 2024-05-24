using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Mapping
{
    public static class AbilitiesMapping
    {
        public static AbilityDto ToDto(this Ability ability)
        {
            return new (
                ability.AbilityId, 
                ability.Name, 
                ability.Description
            );
        }

        public static Ability ToModel(this AbilityDto dto)
        {
            return new Ability
            {
                AbilityId = dto.AbilityId,
                Name = dto.Name,
                Description = dto.Description,
            };
        }

        public static Ability ToModel(this UpdateAbilityDto dto, string id, int abilityId)
        {
            return new Ability
            {
                Id = id,
                AbilityId = abilityId,
                Name = dto.Name,
                Description = dto.Description,
            };
        }
    }
}
