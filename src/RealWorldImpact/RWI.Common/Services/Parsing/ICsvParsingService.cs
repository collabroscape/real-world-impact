using RWI.Common.Models.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Parsing
{
    public interface ICsvParsingService
    {
        List<Zip> ParseZipCsvFile(byte[] contents);
        List<City> ParseCityCsvFile(byte[] contents);
    }
}
