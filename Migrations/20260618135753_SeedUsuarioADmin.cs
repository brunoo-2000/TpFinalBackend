using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpfinalBack.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioADmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PasswordHash", "Rol", "Username" },
                values: new object[] { 1, "AQAAAAIAAYagAAAAEIOK6i7zHIBdyAKNaZZhPvM+rccIN1F5d/C2Xql552mBbkoiEfopOi4g4zcFD4Qw2A==", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
