using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.IdCustomer)
            .HasName("Customer_PK");

        builder.Property(e => e.IdCustomer)
            .UseIdentityColumn();

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(e => e.Contracts)
            .WithOne(e => e.IdCustomerNav)
            .HasForeignKey(e => e.IdCustomer);

        builder.HasMany(e => e.Payments)
            .WithOne(e => e.IdCustomerNav)
            .HasForeignKey(e => e.IdCustomer);
    }
}