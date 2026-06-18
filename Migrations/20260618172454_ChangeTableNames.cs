using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpfinalBack.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Clientes_ClienteId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direcciones",
                table: "Direcciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallePedidos",
                table: "DetallePedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "Producto");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Pedido");

            migrationBuilder.RenameTable(
                name: "Direcciones",
                newName: "Direccion");

            migrationBuilder.RenameTable(
                name: "DetallePedidos",
                newName: "DetallePedido");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedido",
                newName: "IX_Pedido_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedido",
                newName: "IX_Pedido_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_ClienteId",
                table: "Direccion",
                newName: "IX_Direccion_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedidos_ProductoId",
                table: "DetallePedido",
                newName: "IX_DetallePedido_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedidos_PedidoId",
                table: "DetallePedido",
                newName: "IX_DetallePedido_PedidoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direccion",
                table: "Direccion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallePedido",
                table: "DetallePedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedido_Pedido_PedidoId",
                table: "DetallePedido",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedido_Producto_ProductoId",
                table: "DetallePedido",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Direccion_Cliente_ClienteId",
                table: "Direccion",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cliente_ClienteId",
                table: "Pedido",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Usuario_UsuarioId",
                table: "Pedido",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedido_Pedido_PedidoId",
                table: "DetallePedido");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedido_Producto_ProductoId",
                table: "DetallePedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Direccion_Cliente_ClienteId",
                table: "Direccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cliente_ClienteId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Usuario_UsuarioId",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direccion",
                table: "Direccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallePedido",
                table: "DetallePedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Productos");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Direccion",
                newName: "Direcciones");

            migrationBuilder.RenameTable(
                name: "DetallePedido",
                newName: "DetallePedidos");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_UsuarioId",
                table: "Pedidos",
                newName: "IX_Pedidos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Direccion_ClienteId",
                table: "Direcciones",
                newName: "IX_Direcciones_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedido_ProductoId",
                table: "DetallePedidos",
                newName: "IX_DetallePedidos_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedido_PedidoId",
                table: "DetallePedidos",
                newName: "IX_DetallePedidos_PedidoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direcciones",
                table: "Direcciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallePedidos",
                table: "DetallePedidos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoId",
                table: "DetallePedidos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoId",
                table: "DetallePedidos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Clientes_ClienteId",
                table: "Direcciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
