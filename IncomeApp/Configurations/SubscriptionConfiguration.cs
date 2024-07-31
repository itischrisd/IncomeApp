using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(subscription => subscription.IdSubscription)
            .HasName("Subscription_PK");

        builder.Property(subscription => subscription.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(subscription => subscription.DurationInMonths)
            .IsRequired();

        builder.Property(subscription => subscription.LastRenewal)
            .IsRequired();

        builder.Property(subscription => subscription.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.HasOne(subscription => subscription.Customer)
            .WithMany(customer => customer.Subscriptions)
            .HasForeignKey(subscription => subscription.IdCustomer)
            .HasConstraintName("Subscription_Customer_FK");

        builder.HasOne(subscription => subscription.Software)
            .WithMany(software => software.Subscriptions)
            .HasForeignKey(subscription => subscription.IdSoftware)
            .HasConstraintName("Subscription_Software_FK");
    }
}