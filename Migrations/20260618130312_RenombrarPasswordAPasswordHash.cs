using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpfinalBack.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarPasswordAPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Usuarios",
                newName: "Password");
        }
    }
}
