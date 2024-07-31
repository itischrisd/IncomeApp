using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(e => e.IdContract)
            .HasName("Contract_PK");

        builder.Property(e => e.IdContract)
            .UseIdentityColumn();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.Property(e => e.Cost)
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(e => e.YearsOfUpdates)
            .IsRequired();

        builder.Property(e => e.SoftwareVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.IsSigned)
            .HasDefaultValue(false);

        builder.HasOne(e => e.IdCustomerNav)
            .WithMany(e => e.Contracts)
            .HasForeignKey(e => e.IdCustomer);

        builder.HasOne(e => e.IdSoftwareNav)
            .WithMany(e => e.Contracts)
            .HasForeignKey(e => e.IdSoftware);

        var contracts = new List<Contract>
        {
            new()
            {
                IdContract = 1,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 12, 31),
                Cost = 1000,
                YearsOfUpdates = 1,
                SoftwareVersion = "1.0",
                IdCustomer = 1,
                IdSoftware = 1
            },
            new()
            {
                IdContract = 2,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 12, 31),
                Cost = 2000,
                YearsOfUpdates = 2,
                SoftwareVersion = "2.0",
                IdCustomer = 2,
                IdSoftware = 2
            },
            new()
            {
                IdContract = 3,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 12, 31),
                Cost = 3000,
                YearsOfUpdates = 3,
                SoftwareVersion = "3.0",
                IdCustomer = 3,
                IdSoftware = 3
            },
            new()
            {
                IdContract = 4,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 12, 31),
                Cost = 4000,
                YearsOfUpdates = 4,
                SoftwareVersion = "4.0",
                IdCustomer = 4,
                IdSoftware = 1
            },
            new()
            {
                IdContract = 5,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 12, 31),
                Cost = 5000,
                YearsOfUpdates = 1,
                SoftwareVersion = "5.0",
                IdCustomer = 5,
                IdSoftware = 2
            },
            new()
            {
                IdContract = 6,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 12, 31),
                Cost = 6000,
                YearsOfUpdates = 2,
                SoftwareVersion = "6.0",
                IdCustomer = 6,
                IdSoftware = 3
            }
        };
    }
}