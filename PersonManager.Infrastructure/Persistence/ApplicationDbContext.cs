using Microsoft.EntityFrameworkCore;
using PersonManager.Domain.Entities;

namespace PersonManager.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<NaturalPerson> NaturalPersons { get; set; }
        public DbSet<LegalPerson> LegalPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("Persons")
                .HasDiscriminator<string>("PersonType")
                .HasValue<NaturalPerson>("Natural")
                .HasValue<LegalPerson>("Legal");

            modelBuilder.Entity<Person>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Person>()
                .OwnsOne(p => p.Address);

            modelBuilder.Entity<NaturalPerson>()
                .Property(np => np.BirthDate)
                .IsRequired();

            modelBuilder.Entity<LegalPerson>()
                .Property(lp => lp.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            base.OnModelCreating(modelBuilder);
        }
    }
}
