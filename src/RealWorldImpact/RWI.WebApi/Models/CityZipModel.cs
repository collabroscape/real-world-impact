using RWI.Common.Models.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RWI.WebApi.Models
{
    public class CityZipModel
    {
        public string CityCountryCode { get; set; }
        public double CityLatitude { get; set; }
        public double CityLongitude { get; set; }
        public string CityName { get; set; }
        public string CityState { get; set; }

        public string ZipCode { get; set; }
        public string ZipCountryCode { get; set; }
        public double ZipLatitude { get; set; }
        public double ZipLongitude { get; set; }
        public string ZipState { get; set; }

        public static CityZipModel Build(City city, Zip zip)
        {
            return new CityZipModel()
            {
                CityCountryCode = city.CountryCode,
                CityLatitude = city.Latitude,
                CityLongitude = city.Longitude,
                CityName = city.Name,
                CityState = city.State,
                ZipCode = zip.Code,
                ZipCountryCode = zip.CountryCode,
                ZipLatitude = zip.Latitude,
                ZipLongitude = zip.Longitude,
                ZipState = zip.State
            };
        }
    }
}
