using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    IdSubscription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    LastRenewal = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    ContractIdContract = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Subscription_PK", x => x.IdSubscription);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Contracts_ContractIdContract",
                        column: x => x.ContractIdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract");
                    table.ForeignKey(
                        name: "Subscription_Customer_FK",
                        column: x => x.IdCustomer,
                        principalTable: "Customers",
                        principalColumn: "IdCustomer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Subscription_Software_FK",
                        column: x => x.IdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ContractIdContract",
                table: "Subscriptions",
                column: "ContractIdContract");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_IdCustomer",
                table: "Subscriptions",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_IdSoftware",
                table: "Subscriptions",
                column: "IdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
