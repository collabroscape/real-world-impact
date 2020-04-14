using RWI.Common.Extensions.Parsing;
using RWI.Common.Models.Parsing;
using RWI.Common.Services.Parsing;
using RWI.Common.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWI.Common.Services.Location
{
    public class LocationDataService : ILocationDataService
    {
        private readonly IAssetStorageService _assetStorageService;
        private readonly ICsvParsingService _csvParsingService;
        private Random _randomNumberGenerator;

        public LocationDataService(
            IAssetStorageService assetStorageService,
            ICsvParsingService csvParsingService
            )
        {
            _assetStorageService = assetStorageService;
            _csvParsingService = csvParsingService;

            _randomNumberGenerator = new Random();
        }

        public virtual List<City> GetCities()
        {
            byte[] contents = _assetStorageService.GetAssetFile("City.csv");
            return _csvParsingService.ParseCityCsvFile(contents);
        }

        public virtual async Task<List<City>> GetCitiesAsync()
        {
            byte[] contents = await _assetStorageService.GetAssetFileAsync("City.csv");
            return _csvParsingService.ParseCityCsvFile(contents);
        }

        public virtual City GetClosestCity(Zip zip)
        {
            List<City> cities = GetCities();
            return cities.OrderBy(city => city.GetDistanceInMiles(zip)).First();
        }

        public virtual async Task<City> GetClosestCityAsync(Zip zip)
        {
            List<City> cities = await GetCitiesAsync();
            return cities.OrderBy(city => city.GetDistanceInMiles(zip)).First();
        }

        public virtual Zip GetClosestZip(City city)
        {
            List<Zip> zips = GetZips();
            return zips.OrderBy(zip => zip.GetDistanceInMiles(city)).First();
        }

        public virtual async Task<Zip> GetClosestZipAsync(City city)
        {
            List<Zip> zips = await GetZipsAsync();
            return zips.OrderBy(zip => zip.GetDistanceInMiles(city)).First();
        }

        public virtual City GetRandomCity()
        {
            List<City> cities = GetCities();
            int index = _randomNumberGenerator.Next(0, cities.Count);
            return cities[index];
        }

        public virtual async Task<City> GetRandomCityAsync()
        {
            List<City> cities = await GetCitiesAsync();
            int index = _randomNumberGenerator.Next(0, cities.Count);
            return cities[index];
        }

        public virtual Zip GetRandomZip()
        {
            List<Zip> zips = GetZips();
            int index = _randomNumberGenerator.Next(0, zips.Count);
            return zips[index];
        }

        public virtual async Task<Zip> GetRandomZipAsync()
        {
            List<Zip> zips = await GetZipsAsync();
            int index = _randomNumberGenerator.Next(0, zips.Count);
            return zips[index];
        }

        public virtual List<Zip> GetZips()
        {
            byte[] contents = _assetStorageService.GetAssetFile("Zip.csv");
            return _csvParsingService.ParseZipCsvFile(contents);
        }

        public virtual async Task<List<Zip>> GetZipsAsync()
        {
            byte[] contents = await _assetStorageService.GetAssetFileAsync("Zip.csv");
            return _csvParsingService.ParseZipCsvFile(contents);
        }
    }
}
