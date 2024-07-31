using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(c => c.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Pesel)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);

        builder.HasIndex(c => c.Pesel)
            .IsUnique();

        var clients = new List<Person>
        {
            new()
            {
                IdCustomer = 1,
                FirstName = "Sample",
                LastName = "Client",
                Address = "Sample Address",
                Email = "client1@mail.com",
                PhoneNumber = "123456789",
                Pesel = "12345678901"
            },
            new()
            {
                IdCustomer = 2,
                FirstName = "Sample",
                LastName = "Client 2",
                Address = "Sample Address 2",
                Email = "client2@mail.com",
                PhoneNumber = "987654321",
                Pesel = "98765432109"
            },
            new()
            {
                IdCustomer = 3,
                FirstName = "Sample",
                LastName = "Client 3",
                Address = "Sample Address 3",
                Email = "client2@mail.com",
                PhoneNumber = "123456789",
                Pesel = "12345678902"
            }
        };

        builder.HasData(clients);
    }
}