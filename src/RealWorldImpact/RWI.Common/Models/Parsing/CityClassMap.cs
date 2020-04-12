using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Models.Parsing
{
    public class CityClassMap : ClassMap<City>
    {
        public CityClassMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.CountryCode).Name("CountryCode");
            Map(m => m.Latitude).Name("Latitude");
            Map(m => m.Longitude).Name("Longitude");
            Map(m => m.State).Name("State");
        }
    }
}
