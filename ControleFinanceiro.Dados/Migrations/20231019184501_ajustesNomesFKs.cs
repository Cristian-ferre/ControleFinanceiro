using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class ajustesNomesFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosId",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "Despesas",
                newName: "UsuariosUsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Despesas_UsuariosId",
                table: "Despesas",
                newName: "IX_Despesas_UsuariosUsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosUsuarioId",
                table: "Despesas",
                column: "UsuariosUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "UsuariosUsuarioId",
                table: "Despesas",
                newName: "UsuariosId");

            migrationBuilder.RenameIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas",
                newName: "IX_Despesas_UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosId",
                table: "Despesas",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
