using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class ajustandoFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesaParcelas_Despesas_DespesasDespesaId",
                table: "DespesaParcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_FormasPagamento_FormasPagamentoFormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaParcelas_Receitas_ReceitasReceitaId",
                table: "ReceitaParcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosOperacoesLog_Usuarios_UsuariosUsuarioId",
                table: "UsuariosOperacoesLog");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosOperacoesLog_UsuariosUsuarioId",
                table: "UsuariosOperacoesLog");

            migrationBuilder.DropIndex(
                name: "IX_ReceitaParcelas_ReceitasReceitaId",
                table: "ReceitaParcelas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_FormasPagamentoFormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_DespesaParcelas_DespesasDespesaId",
                table: "DespesaParcelas");

            migrationBuilder.DropColumn(
                name: "UsuariosUsuarioId",
                table: "UsuariosOperacoesLog");

            migrationBuilder.DropColumn(
                name: "ReceitasReceitaId",
                table: "ReceitaParcelas");

            migrationBuilder.DropColumn(
                name: "CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "FormasPagamentoFormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesasDespesaId",
                table: "DespesaParcelas");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosOperacoesLog_UsuarioId",
                table: "UsuariosOperacoesLog",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaParcelas_ReceitaId",
                table: "ReceitaParcelas",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_FormaPagamentoId",
                table: "Despesas",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaParcelas_DespesaId",
                table: "DespesaParcelas",
                column: "DespesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaParcelas_Despesas_DespesaId",
                table: "DespesaParcelas",
                column: "DespesaId",
                principalTable: "Despesas",
                principalColumn: "DespesaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_FormasPagamento_FormaPagamentoId",
                table: "Despesas",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "FormaPagamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaParcelas_Receitas_ReceitaId",
                table: "ReceitaParcelas",
                column: "ReceitaId",
                principalTable: "Receitas",
                principalColumn: "ReceitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosOperacoesLog_Usuarios_UsuarioId",
                table: "UsuariosOperacoesLog",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DespesaParcelas_Despesas_DespesaId",
                table: "DespesaParcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_FormasPagamento_FormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaParcelas_Receitas_ReceitaId",
                table: "ReceitaParcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosOperacoesLog_Usuarios_UsuarioId",
                table: "UsuariosOperacoesLog");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosOperacoesLog_UsuarioId",
                table: "UsuariosOperacoesLog");

            migrationBuilder.DropIndex(
                name: "IX_ReceitaParcelas_ReceitaId",
                table: "ReceitaParcelas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_FormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_DespesaParcelas_DespesaId",
                table: "DespesaParcelas");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuariosUsuarioId",
                table: "UsuariosOperacoesLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ReceitasReceitaId",
                table: "ReceitaParcelas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasCategoriaId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormasPagamentoFormaPagamentoId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DespesasDespesaId",
                table: "DespesaParcelas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosOperacoesLog_UsuariosUsuarioId",
                table: "UsuariosOperacoesLog",
                column: "UsuariosUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaParcelas_ReceitasReceitaId",
                table: "ReceitaParcelas",
                column: "ReceitasReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriasCategoriaId",
                table: "Despesas",
                column: "CategoriasCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_FormasPagamentoFormaPagamentoId",
                table: "Despesas",
                column: "FormasPagamentoFormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaParcelas_DespesasDespesaId",
                table: "DespesaParcelas",
                column: "DespesasDespesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DespesaParcelas_Despesas_DespesasDespesaId",
                table: "DespesaParcelas",
                column: "DespesasDespesaId",
                principalTable: "Despesas",
                principalColumn: "DespesaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_CategoriasCategoriaId",
                table: "Despesas",
                column: "CategoriasCategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_FormasPagamento_FormasPagamentoFormaPagamentoId",
                table: "Despesas",
                column: "FormasPagamentoFormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "FormaPagamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaParcelas_Receitas_ReceitasReceitaId",
                table: "ReceitaParcelas",
                column: "ReceitasReceitaId",
                principalTable: "Receitas",
                principalColumn: "ReceitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosOperacoesLog_Usuarios_UsuariosUsuarioId",
                table: "UsuariosOperacoesLog",
                column: "UsuariosUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
