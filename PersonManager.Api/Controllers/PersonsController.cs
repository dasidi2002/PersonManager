using Microsoft.AspNetCore.Mvc;
using PersonManager.Application.DTOs;
using PersonManager.Application.Exceptions;
using PersonManager.Application.Interfaces;

namespace PersonManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            return Ok(person);
        }

        [HttpPost("natural")]
        public async Task<ActionResult<PersonDto>> CreateNaturalPerson(CreateNaturalPersonDto personDto)
        {
            var result = await _personService.CreateNaturalPersonAsync(personDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("legal")]
        public async Task<ActionResult<PersonDto>> CreateLegalPerson(CreateLegalPersonDto personDto)
        {
            var result = await _personService.CreateLegalPersonAsync(personDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("natural/{id}")]
        public async Task<ActionResult<OperationResponseDto>> UpdateNaturalPerson(int id, [FromBody] UpdateNaturalPersonDto personDto)
        {
            var result = await _personService.UpdateNaturalPersonAsync(id, personDto);
            return Ok(result);
        }

        [HttpPut("legal/{id}")]
        public async Task<ActionResult<OperationResponseDto>> UpdateLegalPerson(int id, [FromBody] UpdateLegalPersonDto personDto)
        {
            var result = await _personService.UpdateLegalPersonAsync(id, personDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponseDto>> Delete(int id)
        {
            var result = await _personService.DeletePersonAsync(id);
            return Ok(result);
        }
    }
}