using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.HasKey(e => e.IdSoftware)
            .HasName("Software_PK");

        builder.Property(e => e.IdSoftware)
            .UseIdentityColumn();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(e => e.CurrentVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.YearlyCost)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.HasMany(e => e.Discounts)
            .WithOne(e => e.IdSoftwareNav)
            .HasForeignKey(e => e.IdSoftware);

        builder.HasMany(e => e.Contracts)
            .WithOne(e => e.IdSoftwareNav)
            .HasForeignKey(e => e.IdSoftware);

        var softwares = new List<Software>
        {
            new()
            {
                IdSoftware = 1,
                Name = "Visual Studio",
                Description = "Integrated development environment",
                CurrentVersion = "16.11.7",
                Category = "IDE",
                YearlyCost = 100
            },
            new()
            {
                IdSoftware = 2,
                Name = "Microsoft Office",
                Description = "Office suite",
                CurrentVersion = "16.0.14326.20404",
                Category = "Office",
                YearlyCost = 200
            },
            new()
            {
                IdSoftware = 3,
                Name = "Adobe Photoshop",
                Description = "Raster graphics editor",
                CurrentVersion = "22.5.1",
                Category = "Graphics",
                YearlyCost = 300
            }
        };

        builder.HasData(softwares);
    }
}