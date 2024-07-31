using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IncomeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "IdUser", "Email", "Password", "RefreshToken", "RefreshTokenExp", "Roles", "Salt" },
                values: new object[,]
                {
                    { 3, "admin1@mail.com", "mI5uN+iedEO812e5shKzHyN5plRJgwHvc2FVMaVJmoc=", "", null, "admin", "1UBYrhohNWE3EvyvlBYtNg==" },
                    { 4, "user1@mail.com", "SHboj+Zw3hXxfYq0xftwIeiUHp8bNgLUqxLGU6gvseo=", "", null, "user", "E9pkXFJrthlcl3WamfJsXw==" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "IdUser",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "IdUser",
                keyValue: 4);
        }
    }
}
