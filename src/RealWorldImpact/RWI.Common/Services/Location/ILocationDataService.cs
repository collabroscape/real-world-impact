using RWI.Common.Models.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWI.Common.Services.Location
{
    public interface ILocationDataService
    {
        List<Zip> GetZips();
        Task<List<Zip>> GetZipsAsync();
        Zip GetRandomZip();
        Task<Zip> GetRandomZipAsync();
        Zip GetClosestZip(City city);
        Task<Zip> GetClosestZipAsync(City city);

        List<City> GetCities();
        Task<List<City>> GetCitiesAsync();
        City GetRandomCity();
        Task<City> GetRandomCityAsync();
        City GetClosestCity(Zip zip);
        Task<City> GetClosestCityAsync(Zip zip);

    }
}
