namespace PersonManager.Domain.Entities
{
    public abstract class Person
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string DocumentNumber { get; protected set; }
        public Address Address { get; protected set; }

        protected Person(string name, string documentNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            DocumentNumber = documentNumber;
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}
