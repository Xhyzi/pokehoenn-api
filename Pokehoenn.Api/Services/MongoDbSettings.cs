﻿namespace Pokehoenn.Api.Services
{
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; } = null!;

        public string DbName { get; set; } = null!;

        public string GrowthRateCollectionName { get; set; } = null!;

        public string DexEntryCollectionName { get; set; } = null!;

        public string AbilitiesCollectionName {  get; set; } = null!;

        public string ItemsCollectionName { get; set; } = null!;

        public string MovesCollectionName {  get; set; } = null!;

        public string SpecieCollectionName {  get; set; } = null!;
    }
}
