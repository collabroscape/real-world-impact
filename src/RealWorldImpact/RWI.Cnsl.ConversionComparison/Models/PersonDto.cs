using System;
using System.Collections.Generic;
using System.Text;

namespace RWI.Cnsl.ConversionComparison.Models
{
    public class PersonDto
    {
        public PersonDto()
        {
            Id = Guid.NewGuid();
            FirstName = Id.ToString();
            LastName = Id.ToString();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
