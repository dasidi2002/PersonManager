using System.ComponentModel.DataAnnotations;

namespace PersonManager.Application.DTOs
{
    public class CreateNaturalPersonDto
    {
        [Required(ErrorMessage = "O campo name é obrigatório")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O campo documentNumber é obrigatório")]
        public required string DocumentNumber { get; set; }

        [Required(ErrorMessage = "O campo zipCode é obrigatório")]
        public required string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo birthDate é obrigatório")]
        public required DateTime BirthDate { get; set; }
    }
}