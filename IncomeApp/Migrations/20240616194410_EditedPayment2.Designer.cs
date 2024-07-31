﻿// <auto-generated />
using System;
using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IncomeApp.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240616194410_EditedPayment2")]
    partial class EditedPayment2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IncomeApp.Models.Contract", b =>
                {
                    b.Property<int>("IdContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContract"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.Property<bool>("IsSigned")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("SoftwareVersion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("YearsOfUpdates")
                        .HasColumnType("int");

                    b.HasKey("IdContract")
                        .HasName("Contract_PK");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("IncomeApp.Models.Customer", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomer"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdCustomer")
                        .HasName("Customer_PK");

                    b.ToTable("Customers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Customer");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("IncomeApp.Models.Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Offer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("IdDiscount")
                        .HasName("Discount_PK");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            IdDiscount = 1,
                            EndDate = new DateOnly(2022, 11, 26),
                            IdSoftware = 1,
                            Name = "Black Friday",
                            Offer = "50% off",
                            Percentage = 50,
                            StartDate = new DateOnly(2022, 11, 25)
                        },
                        new
                        {
                            IdDiscount = 2,
                            EndDate = new DateOnly(2022, 12, 25),
                            IdSoftware = 2,
                            Name = "Christmas",
                            Offer = "30% off",
                            Percentage = 30,
                            StartDate = new DateOnly(2022, 12, 24)
                        },
                        new
                        {
                            IdDiscount = 3,
                            EndDate = new DateOnly(2023, 1, 1),
                            IdSoftware = 3,
                            Name = "New Year",
                            Offer = "20% off",
                            Percentage = 20,
                            StartDate = new DateOnly(2022, 12, 31)
                        });
                });

            modelBuilder.Entity("IncomeApp.Models.Payment", b =>
                {
                    b.Property<int>("IdPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPayment"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.HasKey("IdPayment")
                        .HasName("Payment_PK");

                    b.HasIndex("IdContract");

                    b.HasIndex("IdCustomer");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("IncomeApp.Models.Software", b =>
                {
                    b.Property<int>("IdSoftware")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSoftware"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CurrentVersion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("YearlyCost")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("IdSoftware")
                        .HasName("Software_PK");

                    b.ToTable("Softwares");

                    b.HasData(
                        new
                        {
                            IdSoftware = 1,
                            Category = "IDE",
                            CurrentVersion = "16.11.7",
                            Description = "Integrated development environment",
                            Name = "Visual Studio",
                            YearlyCost = 100m
                        },
                        new
                        {
                            IdSoftware = 2,
                            Category = "Office",
                            CurrentVersion = "16.0.14326.20404",
                            Description = "Office suite",
                            Name = "Microsoft Office",
                            YearlyCost = 200m
                        },
                        new
                        {
                            IdSoftware = 3,
                            Category = "Graphics",
                            CurrentVersion = "22.5.1",
                            Description = "Raster graphics editor",
                            Name = "Adobe Photoshop",
                            YearlyCost = 300m
                        });
                });

            modelBuilder.Entity("IncomeApp.Models.Company", b =>
                {
                    b.HasBaseType("IncomeApp.Models.Customer");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasIndex("KRS")
                        .IsUnique()
                        .HasFilter("[KRS] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Company");

                    b.HasData(
                        new
                        {
                            IdCustomer = 4,
                            Address = "Sample Address",
                            Email = "test1@mail.com",
                            PhoneNumber = "123456789",
                            KRS = "1234567890",
                            Name = "Sample Company"
                        },
                        new
                        {
                            IdCustomer = 5,
                            Address = "Sample Address 2",
                            Email = "test2@mail.com",
                            PhoneNumber = "987654321",
                            KRS = "0987654321",
                            Name = "Sample Company 2"
                        },
                        new
                        {
                            IdCustomer = 6,
                            Address = "Sample Address 3",
                            Email = "test3@mail.com",
                            PhoneNumber = "123456789",
                            KRS = "1234567891",
                            Name = "Sample Company 3"
                        });
                });

            modelBuilder.Entity("IncomeApp.Models.Person", b =>
                {
                    b.HasBaseType("IncomeApp.Models.Customer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasIndex("Pesel")
                        .IsUnique()
                        .HasFilter("[Pesel] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Person");

                    b.HasData(
                        new
                        {
                            IdCustomer = 1,
                            Address = "Sample Address",
                            Email = "client1@mail.com",
                            PhoneNumber = "123456789",
                            FirstName = "Sample",
                            IsDeleted = false,
                            LastName = "Client",
                            Pesel = "12345678901"
                        },
                        new
                        {
                            IdCustomer = 2,
                            Address = "Sample Address 2",
                            Email = "client2@mail.com",
                            PhoneNumber = "987654321",
                            FirstName = "Sample",
                            IsDeleted = false,
                            LastName = "Client 2",
                            Pesel = "98765432109"
                        },
                        new
                        {
                            IdCustomer = 3,
                            Address = "Sample Address 3",
                            Email = "client2@mail.com",
                            PhoneNumber = "123456789",
                            FirstName = "Sample",
                            IsDeleted = false,
                            LastName = "Client 3",
                            Pesel = "12345678902"
                        });
                });

            modelBuilder.Entity("IncomeApp.Models.Contract", b =>
                {
                    b.HasOne("IncomeApp.Models.Customer", "IdCustomerNav")
                        .WithMany("Contracts")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IncomeApp.Models.Software", "IdSoftwareNav")
                        .WithMany("Contracts")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdCustomerNav");

                    b.Navigation("IdSoftwareNav");
                });

            modelBuilder.Entity("IncomeApp.Models.Discount", b =>
                {
                    b.HasOne("IncomeApp.Models.Software", "IdSoftwareNav")
                        .WithMany("Discounts")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdSoftwareNav");
                });

            modelBuilder.Entity("IncomeApp.Models.Payment", b =>
                {
                    b.HasOne("IncomeApp.Models.Contract", "IdContractNav")
                        .WithMany("Payments")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IncomeApp.Models.Customer", "IdCustomerNav")
                        .WithMany("Payments")
                        .HasForeignKey("IdCustomer")
                        .IsRequired();

                    b.Navigation("IdContractNav");

                    b.Navigation("IdCustomerNav");
                });

            modelBuilder.Entity("IncomeApp.Models.Contract", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("IncomeApp.Models.Customer", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("IncomeApp.Models.Software", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Discounts");
                });
#pragma warning restore 612, 618
        }
    }
}
