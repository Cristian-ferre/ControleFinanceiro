using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class addAtributoForeignKeyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_UsuarioId",
                table: "Despesas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuarioId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas");

            migrationBuilder.AddColumn<int>(
                name: "CategoriasCategoriaId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuariosUsuarioId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriasCategoriaId",
                table: "Despesas",
                column: "CategoriasCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas",
                column: "UsuariosUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_CategoriasCategoriaId",
                table: "Despesas",
                column: "CategoriasCategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosUsuarioId",
                table: "Despesas",
                column: "UsuariosUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
