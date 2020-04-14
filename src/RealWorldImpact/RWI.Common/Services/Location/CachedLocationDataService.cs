using RWI.Common.Models.Parsing;
using RWI.Common.Services.Caching;
using RWI.Common.Services.Parsing;
using RWI.Common.Services.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Location
{
    public class CachedLocationDataService : LocationDataService, ICachedLocationDataService
    {
        private readonly ICacheService _cacheService;

        public CachedLocationDataService(
            IAssetStorageService assetStorageService,
            ICsvParsingService csvParsingService,
            ICacheService cacheService
            ) : base(
                assetStorageService, 
                csvParsingService
                )
        {
            _cacheService = cacheService;
        }

        public override List<City> GetCities()
        {
            return _cacheService.GetCachedItem("cities", base.GetCities);
        }

        public override async Task<List<City>> GetCitiesAsync()
        {
            return await _cacheService.GetCachedItemAsync("cities", base.GetCitiesAsync);
        }

        public override City GetClosestCity(Zip zip)
        {
            string cacheKey = $"closest-city-{zip.State}-{zip.Code}";
            Func<City> fallback = () =>
            {
                return base.GetClosestCity(zip);
            };
            return _cacheService.GetCachedItem(cacheKey, fallback);
        }

        public override async Task<City> GetClosestCityAsync(Zip zip)
        {
            string cacheKey = $"closest-city-{zip.State}-{zip.Code}";
            Func<Task<City>> fallback = async () =>
            {
                return await base.GetClosestCityAsync(zip);
            };
            return await _cacheService.GetCachedItemAsync(cacheKey, fallback);
        }

        public override Zip GetClosestZip(City city)
        {
            string cacheKey = $"closest-zip-{city.State}-{city.Name}";
            Func<Zip> fallback = () =>
            {
                return base.GetClosestZip(city);
            };
            return _cacheService.GetCachedItem(cacheKey, fallback);
        }

        public override async Task<Zip> GetClosestZipAsync(City city)
        {
            string cacheKey = $"closest-zip-{city.State}-{city.Name}";
            Func<Task<Zip>> fallback = async () =>
            {
                return await base.GetClosestZipAsync(city);
            };
            return await _cacheService.GetCachedItemAsync(cacheKey, fallback);
        }

        public override List<Zip> GetZips()
        {
            return _cacheService.GetCachedItem("zips", base.GetZips);
        }

        public override async Task<List<Zip>> GetZipsAsync()
        {
            return await _cacheService.GetCachedItemAsync("zips", base.GetZipsAsync);
        }
    }
}
