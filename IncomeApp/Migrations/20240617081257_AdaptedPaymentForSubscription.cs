using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeApp.Migrations
{
    /// <inheritdoc />
    public partial class AdaptedPaymentForSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "IdContract",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdSubscription",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdSubscription",
                table: "Payments",
                column: "IdSubscription");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Payment_XOR",
                table: "Payments",
                sql: "(IdContract IS NOT NULL AND IdSubscription IS NULL) OR (IdContract IS NULL AND IdSubscription IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments",
                column: "IdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Subscriptions_IdSubscription",
                table: "Payments",
                column: "IdSubscription",
                principalTable: "Subscriptions",
                principalColumn: "IdSubscription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Subscriptions_IdSubscription",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdSubscription",
                table: "Payments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Payment_XOR",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IdSubscription",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "IdContract",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments",
                column: "IdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
