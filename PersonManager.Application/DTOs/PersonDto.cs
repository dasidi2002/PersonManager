// PersonManager.Application/DTOs/PersonDto.cs
using PersonManager.Application.Enums;

namespace PersonManager.Application.DTOs
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public AddressDto Address { get; set; }
        public PersonType PersonType { get; set; }

        // NaturalPerson specific
        public DateTime? BirthDate { get; set; }

        // LegalPerson specific
        public string CompanyName { get; set; }
    }
}