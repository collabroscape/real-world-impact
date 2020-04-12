using CsvHelper;
using RWI.Common.Models.Parsing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Parsing
{
    public class CsvParsingService : ICsvParsingService
    {
        public List<Zip> ParseZipCsvFile(byte[] contents)
        {
            List<Zip> parsed = null;
            using (MemoryStream stream = new MemoryStream(contents))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Configuration.RegisterClassMap<ZipClassMap>();
                        parsed = csvReader.GetRecords<Zip>().ToList();
                    }
                }
            }
            return parsed;
        }

        public List<City> ParseCityCsvFile(byte[] contents)
        {
            List<City> parsed = null;
            using (MemoryStream stream = new MemoryStream(contents))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Configuration.RegisterClassMap<CityClassMap>();
                        parsed = csvReader.GetRecords<City>().ToList();
                    }
                }
            }
            return parsed;
        }

    }
}
