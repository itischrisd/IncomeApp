using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(e => e.IdDiscount)
            .HasName("Discount_PK");

        builder.Property(e => e.IdDiscount)
            .UseIdentityColumn();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Offer)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Percentage)
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.HasOne(e => e.IdSoftwareNav)
            .WithMany(e => e.Discounts)
            .HasForeignKey(e => e.IdSoftware);

        var discounts = new List<Discount>
        {
            new()
            {
                IdDiscount = 1,
                Name = "Black Friday",
                Offer = "50% off",
                Percentage = 50,
                StartDate = new DateOnly(2022, 11, 25),
                EndDate = new DateOnly(2022, 11, 26),
                IdSoftware = 1
            },
            new()
            {
                IdDiscount = 2,
                Name = "Christmas",
                Offer = "30% off",
                Percentage = 30,
                StartDate = new DateOnly(2022, 12, 24),
                EndDate = new DateOnly(2022, 12, 25),
                IdSoftware = 2
            },
            new()
            {
                IdDiscount = 3,
                Name = "New Year",
                Offer = "20% off",
                Percentage = 20,
                StartDate = new DateOnly(2022, 12, 31),
                EndDate = new DateOnly(2023, 1, 1),
                IdSoftware = 3
            }
        };

        builder.HasData(discounts);
    }
}