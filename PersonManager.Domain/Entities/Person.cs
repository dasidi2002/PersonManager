namespace PersonManager.Domain.Entities
{
    public abstract class Person
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string DocumentNumber { get; protected set; }
        public Address Address { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected Person() { }

        protected Person(string name, string documentNumber)
        {
            Name = name;
            DocumentNumber = documentNumber;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}