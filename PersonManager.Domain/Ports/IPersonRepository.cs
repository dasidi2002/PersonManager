using PersonManager.Domain.Entities;

namespace PersonManager.Domain.Ports
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}