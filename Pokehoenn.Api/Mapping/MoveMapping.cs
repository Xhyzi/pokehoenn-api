using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Models;


namespace Pokehoenn.Api.Mapping
{
    public static class MoveMapping
    {
        public static MoveDto ToDto(this Move move)
        {
            return new (
                move.Name,
                move.Description,
                move.Type,
                move.Power,
                move.Accuracy,
                move.Pp,
                move.Priority,
                move.Split,
                move.Targets,
                move.Effect,
                move.EffectChance,
                move.Flags
            );
        }

        public static Move ToModel(this MoveDto dto)
        {
            return new Move
            {
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                Power = dto.Power,
                Accuracy = dto.Accuracy,
                Pp = dto.Pp,
                Priority = dto.Priority,
                Split = dto.Split,
                Targets = dto.Targets,
                Effect = dto.Effect,
                EffectChance = dto.EffectChance,
                Flags = dto.Flags
            };
        }

        public static Move ToModel(this UpdateMoveDto dto, string id, string name)
        {
            return new Move
            {
                Id = id,
                Name = name,
                Description = dto.Description,
                Type = dto.Type,
                Power = dto.Power,
                Accuracy = dto.Accuracy,
                Pp = dto.Pp,
                Priority = dto.Priority,
                Split = dto.Split,
                Targets = dto.Targets,
                Effect = dto.Effect,
                EffectChance = dto.EffectChance,
                Flags = dto.Flags
            };
        }
    }
}
