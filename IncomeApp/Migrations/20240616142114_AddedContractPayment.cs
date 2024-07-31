using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedContractPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "YearlyCost",
                table: "Softwares",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsSigned",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 1,
                column: "YearlyCost",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 2,
                column: "YearlyCost",
                value: 200m);

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 3,
                column: "YearlyCost",
                value: 300m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearlyCost",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "IsSigned",
                table: "Contracts");
        }
    }
}
