using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.IdPayment)
            .HasName("Payment_PK");

        builder.Property(e => e.IdPayment)
            .UseIdentityColumn();

        builder.Property(e => e.Amount)
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.HasOne(e => e.IdContractNav)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.IdContract);

        builder.HasOne(e => e.IdCustomerNav)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.IdCustomer)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(e => e.IdSubscriptionNav)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.IdSubscription);

        builder.Property(e => e.IdContract)
            .IsRequired(false);

        builder.Property(e => e.IdSubscription)
            .IsRequired(false);

        builder.ToTable(t => t.HasCheckConstraint("CK_Payment_XOR",
            "(IdContract IS NOT NULL AND IdSubscription IS NULL) OR (IdContract IS NULL AND IdSubscription IS NOT NULL)"));
    }
}