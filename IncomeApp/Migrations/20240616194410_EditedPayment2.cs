using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeApp.Migrations
{
    /// <inheritdoc />
    public partial class EditedPayment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Contracts_IdContract",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_IdContract",
                table: "Payments",
                newName: "IX_Payments_IdContract");

            migrationBuilder.AddColumn<int>(
                name: "IdCustomer",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments",
                column: "IdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments",
                column: "IdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_IdCustomer",
                table: "Payments",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "IdCustomer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_IdCustomer",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdCustomer",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IdCustomer",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_IdContract",
                table: "Payment",
                newName: "IX_Payment_IdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Contracts_IdContract",
                table: "Payment",
                column: "IdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
