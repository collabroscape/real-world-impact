using AutoMapper;
using RWI.Cnsl.ConversionComparison.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Cnsl.ConversionComparison.Extensions
{
    public static class PersonMapper
    {
        public static void Build(IMapperConfigurationExpression ce)
        {
            ce.CreateMap<Person, PersonDto>();
            ce.CreateMap<PersonDto, Person>();
        }
    }
}
