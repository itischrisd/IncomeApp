using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeApp.Migrations
{
    /// <inheritdoc />
    public partial class SmallFixInSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Contracts_ContractIdContract",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ContractIdContract",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ContractIdContract",
                table: "Subscriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractIdContract",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ContractIdContract",
                table: "Subscriptions",
                column: "ContractIdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Contracts_ContractIdContract",
                table: "Subscriptions",
                column: "ContractIdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract");
        }
    }
}
