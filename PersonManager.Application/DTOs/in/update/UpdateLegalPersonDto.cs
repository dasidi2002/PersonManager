using System.ComponentModel.DataAnnotations;

namespace PersonManager.Application.DTOs
{
    public class UpdateLegalPersonDto
    {
        public string? Name { get; set; }

        [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo documentNumber deve ter exatamente 14 caracteres")]
        public string? DocumentNumber { get; set; }
        public string? ZipCode { get; set; }
        public string? CompanyName { get; set; }
    }
}
