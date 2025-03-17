using PersonManager.Application.DTOs;

namespace PersonManager.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<Guid> CreateNaturalPersonAsync(PersonDto personDto);
        Task<Guid> CreateLegalPersonAsync(PersonDto personDto);
        Task UpdatePersonAsync(Guid id, PersonDto personDto);
        Task DeletePersonAsync(Guid id);
    }
}