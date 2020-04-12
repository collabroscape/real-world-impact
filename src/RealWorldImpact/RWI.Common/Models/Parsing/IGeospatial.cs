using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Models.Parsing
{
    public interface IGeospatial
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
