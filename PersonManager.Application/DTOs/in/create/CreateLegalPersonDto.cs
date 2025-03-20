using System.ComponentModel.DataAnnotations;

namespace PersonManager.Application.DTOs
{
    public class CreateLegalPersonDto
    {
        [Required(ErrorMessage = "O campo name é obrigatório")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O campo documentNumber é obrigatório")]
        public required string DocumentNumber { get; set; }

        [Required(ErrorMessage = "O campo zipCode é obrigatório")]
        public required string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo companyName é obrigatório")]
        public required string CompanyName { get; set; }
    }
}