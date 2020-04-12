using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Models.Parsing
{
    public class City : IGeospatial
    {
        public string CountryCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
    }
}
