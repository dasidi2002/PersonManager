using PersonManager.Application.Enums;

namespace PersonManager.Application.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public AddressDto Address { get; set; }
        public PersonType PersonType { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CompanyName { get; set; }
    }
}