namespace PersonManager.Application.DTOs
{
    public class UpdateNaturalPersonDto
    {
        public string? Name { get; set; }
        public string? DocumentNumber { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}