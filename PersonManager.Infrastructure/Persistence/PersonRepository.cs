using Microsoft.EntityFrameworkCore;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonManager.Infrastructure.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _dbContext.Persons
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbContext.Persons
                .Include(p => p.Address)
                .ToListAsync();
        }

        public async Task<Person> AddAsync(Person person)
        {
            await _dbContext.Persons.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task UpdateAsync(Person person)
        {
            _dbContext.Persons.Update(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await GetByIdAsync(id);
            if (person != null)
            {
                _dbContext.Persons.Remove(person);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
