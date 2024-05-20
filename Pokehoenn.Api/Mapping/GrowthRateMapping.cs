using Pokehoenn.Api.Dtos;
using Pokehoenn.Api.Models;

namespace Pokehoenn.Api.Mapping
{
    public static class GrowthRateMapping
    {
        public static GrowthRateDto ToDto(this GrowthRate model)
        {
            return new(
                model.Name,
                model.Formula,
                model.MaxExp
            );
        }
    }
}
