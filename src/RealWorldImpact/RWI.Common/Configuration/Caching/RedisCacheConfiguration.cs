using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Common.Configuration.Caching
{
    public class RedisCacheConfiguration
    {
        public string DatabaseIndex { get; set; }
        public string CacheDuration { get; set; }

        public int DatabaseIndexValue
        {
            get
            {
                int value = 0;
                int.TryParse(DatabaseIndex, out value);
                return value;
            }
        }

        public double CacheDurationValue
        {
            get
            {
                double value = 0;
                double.TryParse(CacheDuration, out value);
                return value;
            }
        }

    }
}
