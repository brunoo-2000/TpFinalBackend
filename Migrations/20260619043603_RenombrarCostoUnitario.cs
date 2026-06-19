using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpfinalBack.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarCostoUnitario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConstoUnitario",
                table: "DetallePedido",
                newName: "CostoUnitario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostoUnitario",
                table: "DetallePedido",
                newName: "ConstoUnitario");
        }
    }
}
