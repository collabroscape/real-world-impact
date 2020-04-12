using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Models.Parsing
{
    public class ZipClassMap : ClassMap<Zip>
    {
        public ZipClassMap()
        {
            Map(m => m.Code).Name("Code");
            Map(m => m.CountryCode).Name("CountryCode");
            Map(m => m.Latitude).Name("Latitude");
            Map(m => m.Longitude).Name("Longitude");
            Map(m => m.State).Name("State");
        }
    }
}
