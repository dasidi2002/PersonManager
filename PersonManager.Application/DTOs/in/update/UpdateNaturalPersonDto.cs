using System.ComponentModel.DataAnnotations;

namespace PersonManager.Application.DTOs
{
    public class UpdateNaturalPersonDto
    {
        public string? Name { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo documentNumber deve ter exatamente 11 caracteres")]
        public string? DocumentNumber { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}