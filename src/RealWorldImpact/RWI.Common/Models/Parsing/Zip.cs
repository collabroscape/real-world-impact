using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Models.Parsing
{
    public class Zip : IGeospatial
    {
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string State { get; set; }
    }
}
