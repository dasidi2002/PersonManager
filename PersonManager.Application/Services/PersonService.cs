using AutoMapper;
using PersonManager.Application.DTOs;
using PersonManager.Application.Exceptions;
using PersonManager.Application.Interfaces;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Ports;

namespace PersonManager.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IAddressService addressService, IMapper mapper)
        {
            _personRepository = personRepository;
            _addressService = addressService;
            _mapper = mapper;
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new PersonNotFoundException(id);

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }

        public async Task<PersonDto> CreateNaturalPersonAsync(CreateNaturalPersonDto personDto)
        {
            try
            {
                var address = await GetAddressAsync(personDto.ZipCode);
                var person = _mapper.Map<NaturalPerson>(personDto);
                person.UpdateAddress(address);
                var createdPerson = await _personRepository.AddAsync(person);

                return _mapper.Map<PersonDto>(createdPerson);
            }
            catch (HttpRequestException)
            {
                throw new ZipCodeNotFoundException(personDto.ZipCode);
            }
        }

        public async Task<PersonDto> CreateLegalPersonAsync(CreateLegalPersonDto personDto)
        {
            try
            {
                var address = await GetAddressAsync(personDto.ZipCode);
                var person = _mapper.Map<LegalPerson>(personDto);
                person.UpdateAddress(address);
                var createdPerson = await _personRepository.AddAsync(person);

                return _mapper.Map<PersonDto>(createdPerson);
            }
            catch (HttpRequestException)
            {
                throw new ZipCodeNotFoundException(personDto.ZipCode);
            }
        }

        public async Task<OperationResponseDto> UpdateNaturalPersonAsync(int id, UpdateNaturalPersonDto personDto)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new PersonNotFoundException(id);

            if (!(person is NaturalPerson naturalPerson))
                throw PersonTypeException.NotNaturalPerson;

            if (!string.IsNullOrEmpty(personDto.ZipCode))
            {
                try
                {
                    var address = await GetAddressAsync(personDto.ZipCode);
                    naturalPerson.UpdateAddress(address);
                }
                catch (HttpRequestException)
                {
                    throw new ZipCodeNotFoundException(personDto.ZipCode);
                }
            }

            _mapper.Map(personDto, naturalPerson); // Atualiza apenas os campos fornecidos
            await _personRepository.UpdateAsync(naturalPerson);

            return _mapper.Map<OperationResponseDto>(naturalPerson);
        }

        public async Task<OperationResponseDto> UpdateLegalPersonAsync(int id, UpdateLegalPersonDto personDto)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new PersonNotFoundException(id);

            if (!(person is LegalPerson legalPerson))
                throw PersonTypeException.NotLegalPerson;

            if (!string.IsNullOrEmpty(personDto.ZipCode))
            {
                try
                {
                    var address = await GetAddressAsync(personDto.ZipCode);
                    legalPerson.UpdateAddress(address);
                }
                catch (HttpRequestException)
                {
                    throw new ZipCodeNotFoundException(personDto.ZipCode);
                }
            }

            _mapper.Map(personDto, legalPerson); // Atualiza apenas os campos fornecidos
            await _personRepository.UpdateAsync(legalPerson);

            return _mapper.Map<OperationResponseDto>(legalPerson);
        }

        public async Task<OperationResponseDto> DeletePersonAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new PersonNotFoundException(id);

            var response = _mapper.Map<OperationResponseDto>(person);
            response.Message = "Pessoa excluída com sucesso";

            await _personRepository.DeleteAsync(id);

            return response;
        }

        private async Task<Address> GetAddressAsync(string zipCode)
        {
            return await _addressService.GetAddressByZipCodeAsync(zipCode);
        }
    }
}