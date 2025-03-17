using PersonManager.Application.DTOs;
using PersonManager.Application.Enums;
using PersonManager.Application.Interfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Ports;

namespace PersonManager.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressService _addressService;

        public PersonService(IPersonRepository personRepository, IAddressService addressService)
        {
            _personRepository = personRepository;
            _addressService = addressService;
        }

        public async Task<PersonDto> GetByIdAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            return MapToDto(person);
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync();
            return persons.Select(MapToDto);
        }

        public async Task<Guid> CreateNaturalPersonAsync(PersonDto personDto)
        {
            var address = await GetAddressAsync(personDto.Address.ZipCode);

            var person = new NaturalPerson(
                personDto.Name,
                personDto.DocumentNumber,
                personDto.BirthDate.Value
            );

            person.UpdateAddress(address);
            await _personRepository.AddAsync(person);

            return person.Id;
        }

        public async Task<Guid> CreateLegalPersonAsync(PersonDto personDto)
        {
            var address = await GetAddressAsync(personDto.Address.ZipCode);

            var person = new LegalPerson(
                personDto.Name,
                personDto.CompanyName,
                personDto.DocumentNumber
            );

            person.UpdateAddress(address);
            await _personRepository.AddAsync(person);

            return person.Id;
        }

        public async Task UpdatePersonAsync(Guid id, PersonDto personDto)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null)
                throw new KeyNotFoundException($"Person with id {id} not found");

            var address = await GetAddressAsync(personDto.Address.ZipCode);
            person.UpdateAddress(address);

            await _personRepository.UpdateAsync(person);
        }

        public async Task DeletePersonAsync(Guid id)
        {
            await _personRepository.DeleteAsync(id);
        }

        private async Task<Address> GetAddressAsync(string zipCode)
        {
            return await _addressService.GetAddressByZipCodeAsync(zipCode);
        }

        private PersonDto MapToDto(Person person)
        {
            var dto = new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                DocumentNumber = person.DocumentNumber,
                Address = new AddressDto
                {
                    Street = person.Address.Street,
                    Number = person.Address.Number,
                    Complement = person.Address.Complement,
                    Neighborhood = person.Address.Neighborhood,
                    City = person.Address.City,
                    State = person.Address.State,
                    ZipCode = person.Address.ZipCode
                }
            };

            if (person is NaturalPerson naturalPerson)
            {
                dto.PersonType = PersonType.Natural;
                dto.BirthDate = naturalPerson.BirthDate;
            }
            else if (person is LegalPerson legalPerson)
            {
                dto.PersonType = PersonType.Legal;
                dto.CompanyName = legalPerson.CompanyName;
            }

            return dto;
        }
    }
}