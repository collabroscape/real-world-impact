using RWI.Cnsl.ConversionComparison.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Cnsl.ConversionComparison.Extensions
{
    public static class PersonExtensions
    {
        public static Person ConvertToPerson(this PersonDto personDto)
        {
            return new Person()
            {
                Id = personDto.Id.ToString(),
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                FullName = $"{personDto.FirstName} {personDto.LastName}"
            };
        }

        public static PersonDto ConvertToPersonDto(this Person person)
        {
            return new PersonDto()
            {
                Id = Guid.Parse(person.Id),
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }
    }
}
