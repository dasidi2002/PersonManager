namespace PersonManager.Domain.Entities
{
    public class LegalPerson : Person
    {
        public string CompanyName { get; private set; }
        public string Cnpj { get => DocumentNumber; }

        protected LegalPerson() { }

        public LegalPerson(string name, string companyName, string cnpj)
            : base(name, cnpj)
        {
            CompanyName = companyName;
        }
    }
}