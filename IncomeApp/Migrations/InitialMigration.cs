#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IncomeApp.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Customers",
            table => new
            {
                IdCustomer = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Address = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                PhoneNumber = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                Discriminator = table.Column<string>("nvarchar(8)", maxLength: 8, nullable: false),
                Name = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                KRS = table.Column<string>("nvarchar(14)", maxLength: 14, nullable: true),
                FirstName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                LastName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                Pesel = table.Column<string>("nvarchar(11)", maxLength: 11, nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: true, defaultValue: false)
            },
            constraints: table => { table.PrimaryKey("Customer_PK", x => x.IdCustomer); });

        migrationBuilder.CreateTable(
            "Softwares",
            table => new
            {
                IdSoftware = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: false),
                CurrentVersion = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Category = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table => { table.PrimaryKey("Software_PK", x => x.IdSoftware); });

        migrationBuilder.CreateTable(
            "Contracts",
            table => new
            {
                IdContract = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StartDate = table.Column<DateOnly>("date", nullable: false),
                EndDate = table.Column<DateOnly>("date", nullable: false),
                Cost = table.Column<decimal>("decimal(10,2)", nullable: false),
                YearsOfUpdates = table.Column<int>("int", nullable: false),
                SoftwareVersion = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                IdCustomer = table.Column<int>("int", nullable: false),
                IdSoftware = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("Contract_PK", x => x.IdContract);
                table.ForeignKey(
                    "FK_Contracts_Customers_IdCustomer",
                    x => x.IdCustomer,
                    "Customers",
                    "IdCustomer",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_Contracts_Softwares_IdSoftware",
                    x => x.IdSoftware,
                    "Softwares",
                    "IdSoftware",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Discounts",
            table => new
            {
                IdDiscount = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Offer = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Percentage = table.Column<int>("int", nullable: false),
                StartDate = table.Column<DateOnly>("date", nullable: false),
                EndDate = table.Column<DateOnly>("date", nullable: false),
                IdSoftware = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("Discount_PK", x => x.IdDiscount);
                table.ForeignKey(
                    "FK_Discounts_Softwares_IdSoftware",
                    x => x.IdSoftware,
                    "Softwares",
                    "IdSoftware",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            "Customers",
            new[]
            {
                "IdCustomer", "Address", "Discriminator", "Email", "FirstName", "LastName", "Pesel", "PhoneNumber"
            },
            new object[,]
            {
                { 1, "Sample Address", "Person", "client1@mail.com", "Sample", "Client", "12345678901", "123456789" },
                {
                    2, "Sample Address 2", "Person", "client2@mail.com", "Sample", "Client 2", "98765432109",
                    "987654321"
                },
                {
                    3, "Sample Address 3", "Person", "client2@mail.com", "Sample", "Client 3", "12345678902",
                    "123456789"
                }
            });

        migrationBuilder.InsertData(
            "Customers",
            new[] { "IdCustomer", "Address", "Discriminator", "Email", "KRS", "Name", "PhoneNumber" },
            new object[,]
            {
                { 4, "Sample Address", "Company", "test1@mail.com", "1234567890", "Sample Company", "123456789" },
                { 5, "Sample Address 2", "Company", "test2@mail.com", "0987654321", "Sample Company 2", "987654321" },
                { 6, "Sample Address 3", "Company", "test3@mail.com", "1234567891", "Sample Company 3", "123456789" }
            });

        migrationBuilder.InsertData(
            "Softwares",
            new[] { "IdSoftware", "Category", "CurrentVersion", "Description", "Name" },
            new object[,]
            {
                { 1, "IDE", "16.11.7", "Integrated development environment", "Visual Studio" },
                { 2, "Office", "16.0.14326.20404", "Office suite", "Microsoft Office" },
                { 3, "Graphics", "22.5.1", "Raster graphics editor", "Adobe Photoshop" }
            });

        migrationBuilder.InsertData(
            "Discounts",
            new[] { "IdDiscount", "EndDate", "IdSoftware", "Name", "Offer", "Percentage", "StartDate" },
            new object[,]
            {
                { 1, new DateOnly(2022, 11, 26), 1, "Black Friday", "50% off", 50, new DateOnly(2022, 11, 25) },
                { 2, new DateOnly(2022, 12, 25), 2, "Christmas", "30% off", 30, new DateOnly(2022, 12, 24) },
                { 3, new DateOnly(2023, 1, 1), 3, "New Year", "20% off", 20, new DateOnly(2022, 12, 31) }
            });

        migrationBuilder.CreateIndex(
            "IX_Contracts_IdCustomer",
            "Contracts",
            "IdCustomer");

        migrationBuilder.CreateIndex(
            "IX_Contracts_IdSoftware",
            "Contracts",
            "IdSoftware");

        migrationBuilder.CreateIndex(
            "IX_Customers_KRS",
            "Customers",
            "KRS",
            unique: true,
            filter: "[KRS] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_Customers_Pesel",
            "Customers",
            "Pesel",
            unique: true,
            filter: "[Pesel] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_Discounts_IdSoftware",
            "Discounts",
            "IdSoftware");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Contracts");

        migrationBuilder.DropTable(
            "Discounts");

        migrationBuilder.DropTable(
            "Customers");

        migrationBuilder.DropTable(
            "Softwares");
    }
}