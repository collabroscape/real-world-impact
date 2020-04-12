using RWI.Common.Models.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using RWI.Common.Extensions;

namespace RWI.Common.Extensions.Parsing
{
    public static class IGeospatialExtensions
    {
        public static double GetDistanceInMiles(this IGeospatial from, IGeospatial to)
        {
            // Based on https://www.geodatasource.com/developers/c-sharp

            double theta = from.Longitude - to.Longitude;
            double dist =
                Math.Sin(from.Latitude.ConvertDegreesToRadians()) * Math.Sin(to.Latitude.ConvertDegreesToRadians()) +
                Math.Cos(from.Latitude.ConvertDegreesToRadians()) * Math.Cos(to.Latitude.ConvertDegreesToRadians()) *
                Math.Cos(theta.ConvertDegreesToRadians());
            dist = Math.Acos(dist).ConvertRadiansToDegrees();
            dist = dist * 60 * 1.1515;
            return (dist);
        }
    }
}
