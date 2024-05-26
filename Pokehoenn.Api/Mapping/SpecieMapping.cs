using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Mapping
{
    public static class SpecieMapping
    {
        public static SpecieDto ToDto(this Specie specie)
        {
            return new SpecieDto
                (
                specie.Name,
                specie.DexNum,
                specie.Types,
                specie.BaseHp,
                specie.BaseAtk,
                specie.BaseDef,
                specie.BaseSpd,
                specie.BaseSpAtk,
                specie.BaseSpDef,
                specie.Abilities,
                specie.HiddenAbility,
                specie.CatchRate,
                specie.GrowthRate,
                specie.ExpYield,
                specie.EvYield,
                specie.FemaleRatio,
                specie.EggCycles,
                specie.EggGroups,
                specie.Friendship,
                specie.BodyColor,
                specie.Items,
                specie.SafariFleeRate,
                specie.Flags
            );
        }

        public static Specie ToModel(this SpecieDto dto)
        {
            return new Specie()
            {
                Name = dto.Name,
                DexNum = dto.DexNum,
                Types = dto.Types,
                BaseHp = dto.BaseHp,
                BaseAtk = dto.BaseAtk,
                BaseDef = dto.BaseDef,
                BaseSpd = dto.BaseSpd,
                BaseSpAtk = dto.BaseSpAtk,
                BaseSpDef = dto.BaseSpDef,
                Abilities = dto.Abilities,
                HiddenAbility = dto.HiddenAbility,
                CatchRate = dto.CatchRate,
                GrowthRate = dto.GrowthRate,
                ExpYield = dto.ExpYield,
                EvYield = dto.EvYield,
                FemaleRatio = dto.FemaleRatio,
                EggCycles = dto.EggCycles,
                EggGroups = dto.EggGroups,
                Friendship = dto.Friendship,
                BodyColor = dto.BodyColor,
                Items = dto.Items,
                SafariFleeRate = dto.SafariFleeRate,
                Flags = dto.Flags
            };
        }
    }
}
