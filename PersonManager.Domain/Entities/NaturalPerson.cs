namespace PersonManager.Domain.Entities
{
    public class NaturalPerson : Person
    {
        public DateTime BirthDate { get; private set; }
        public string Cpf { get => DocumentNumber; }

        protected NaturalPerson() { }

        public NaturalPerson(string name, string cpf, DateTime birthDate)
            : base(name, cpf)
        {
            BirthDate = birthDate;
        }

    }
}