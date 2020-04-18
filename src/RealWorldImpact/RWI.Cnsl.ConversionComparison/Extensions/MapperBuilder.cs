using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Cnsl.ConversionComparison.Extensions
{
    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            var config = new MapperConfiguration(config =>
            {
                PersonMapper.Build(config);
            });
            return config.CreateMapper();
        }
    }
}
