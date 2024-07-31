using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.KRS)
            .HasMaxLength(14)
            .IsRequired();

        builder.HasIndex(c => c.KRS)
            .IsUnique();

        var companies = new List<Company>
        {
            new()
            {
                IdCustomer = 4,
                Name = "Sample Company",
                Address = "Sample Address",
                Email = "test1@mail.com",
                PhoneNumber = "123456789",
                KRS = "1234567890"
            },
            new()
            {
                IdCustomer = 5,
                Name = "Sample Company 2",
                Address = "Sample Address 2",
                Email = "test2@mail.com",
                PhoneNumber = "987654321",
                KRS = "0987654321"
            },
            new()
            {
                IdCustomer = 6,
                Name = "Sample Company 3",
                Address = "Sample Address 3",
                Email = "test3@mail.com",
                PhoneNumber = "123456789",
                KRS = "1234567891"
            }
        };

        builder.HasData(companies);
    }
}