using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class ajustandoUsuarioIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas",
                column: "UsuarioId");

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
                name: "FK_Despesas_Usuarios_UsuarioId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuariosUsuarioId",
                table: "Despesas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas",
                column: "UsuariosUsuarioId");

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
