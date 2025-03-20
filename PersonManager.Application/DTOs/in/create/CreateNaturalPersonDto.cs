using System.ComponentModel.DataAnnotations;
namespace PersonManager.Application.DTOs
{
    public class CreateNaturalPersonDto
    {
        [Required(ErrorMessage = "O campo name é obrigatório")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O campo documentNumber é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo documentNumber deve ter exatamente 11 caracteres")]
        public required string DocumentNumber { get; set; }

        [Required(ErrorMessage = "O campo zipCode é obrigatório")]
        public required string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo birthDate é obrigatório")]
        public required DateTime BirthDate { get; set; }
    }
}