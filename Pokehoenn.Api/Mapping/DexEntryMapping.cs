using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Mapping
{
    public static class DexEntryMapping
    {
        public static DexEntryDto ToDto(this DexEntry model)
        {
            return new (
                model.Species, 
                model.NationalId, 
                model.RegionalId, 
                model.CategoryName, 
                model.Height, 
                model.Weight, 
                model.Description
            );
        }

        public static DexEntry ToModel(this DexEntryDto dto)
        {
            return new DexEntry
            {
                Species = dto.Species,
                NationalId = dto.NationalId,
                RegionalId = dto.RegionalId,
                CategoryName = dto.CategoryName,
                Height = dto.Height,
                Weight = dto.Weight,
                Description = dto.Description
            };
        }
    }
}
