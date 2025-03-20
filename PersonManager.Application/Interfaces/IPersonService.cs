using PersonManager.Application.DTOs;

namespace PersonManager.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> GetByIdAsync(int id);
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<PersonDto> CreateNaturalPersonAsync(CreateNaturalPersonDto personDto);
        Task<PersonDto> CreateLegalPersonAsync(CreateLegalPersonDto personDto);
        Task<OperationResponseDto> UpdateNaturalPersonAsync(int id, UpdateNaturalPersonDto personDto);
        Task<OperationResponseDto> UpdateLegalPersonAsync(int id, UpdateLegalPersonDto personDto);
        Task<OperationResponseDto> DeletePersonAsync(int id);
    }
}