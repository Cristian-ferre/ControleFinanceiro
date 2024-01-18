using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class addAtributoForeignKeyReceitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receitas_Usuarios_UsuariosUsuarioId",
                table: "Receitas");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_UsuariosUsuarioId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "UsuariosUsuarioId",
                table: "Receitas");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuarioId",
                table: "Receitas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receitas_Usuarios_UsuarioId",
                table: "Receitas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receitas_Usuarios_UsuarioId",
                table: "Receitas");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_UsuarioId",
                table: "Receitas");

            migrationBuilder.AddColumn<int>(
                name: "UsuariosUsuarioId",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuariosUsuarioId",
                table: "Receitas",
                column: "UsuariosUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receitas_Usuarios_UsuariosUsuarioId",
                table: "Receitas",
                column: "UsuariosUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
