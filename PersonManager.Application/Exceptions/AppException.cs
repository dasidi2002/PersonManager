namespace PersonManager.Application.Exceptions
{
    // Classe base de exceção
    public class AppException : Exception
    {
        public int StatusCode { get; }

        public AppException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    // Exceções específicas
    public class PersonNotFoundException : AppException
    {
        public PersonNotFoundException() : base("Id não encontrado", 404) { }
        public PersonNotFoundException(int id) : base($"Pessoa com Id {id} não encontrada", 404) { }
    }

    public class ZipCodeNotFoundException : AppException
    {
        public ZipCodeNotFoundException(string zipCode) : base($"CEP {zipCode} não encontrado") { }
    }

    public class PersonTypeException : AppException
    {
        public static PersonTypeException NotNaturalPerson => new PersonTypeException("A entidade não é uma Pessoa Física", 400);
        public static PersonTypeException NotLegalPerson => new PersonTypeException("A entidade não é uma Pessoa Jurídica", 400);

        private PersonTypeException(string message, int statusCode) : base(message, statusCode) { }
    }
}