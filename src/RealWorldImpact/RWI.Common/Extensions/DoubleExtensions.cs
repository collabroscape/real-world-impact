using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Extensions
{
    public static class DoubleExtensions
    {
        public static double ConvertDistanceToKm(this double distanceInMi)
        {
            return distanceInMi * 1.609344;
        }

        public static double ConvertDistanceToNautical(this double distanceInMi)
        {
            return distanceInMi * 0.8684;
        }

        public static double ConvertDegreesToRadians(this double degrees)
        {
            return (degrees * Math.PI / 180.0);
        }

        public static double ConvertRadiansToDegrees(this double radians)
        {
            return (radians / Math.PI * 180.0);
        }
    }
}
