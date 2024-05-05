using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class addNewModelagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ReceitaDataFim",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ReceitaValor",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "DespesaValor",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesasDataFim",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "ReceitaData",
                table: "Receitas",
                newName: "ReceitaDataVencimento");

            migrationBuilder.RenameColumn(
                name: "StatusDespesas",
                table: "Despesas",
                newName: "FormasPagamentoFormaPagamentoId");

            migrationBuilder.RenameColumn(
                name: "DespesasData",
                table: "Despesas",
                newName: "DespesasDataInclusao");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceitaDataInclusao",
                table: "Receitas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ReceitaDeletado",
                table: "Receitas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReceitaQuantidadeParcelas",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasCategoriaId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DespesaDataVencimento",
                table: "Despesas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DespesaDeletado",
                table: "Despesas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DespesaQuantidadeParcelas",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormaPagamentoId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuariosUsuarioId",
                table: "Despesas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "CategoriaDeletado",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuariosUsuarioId",
                table: "Categorias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DespesaParcelas",
                columns: table => new
                {
                    DespesaParcelaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DespesaValor = table.Column<double>(type: "float", nullable: true),
                    DespesaParcelaDeletado = table.Column<bool>(type: "bit", nullable: false),
                    StatusDespesas = table.Column<int>(type: "int", nullable: false),
                    DespesaId = table.Column<int>(type: "int", nullable: false),
                    DespesasDespesaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaParcelas", x => x.DespesaParcelaId);
                    table.ForeignKey(
                        name: "FK_DespesaParcelas_Despesas_DespesasDespesaId",
                        column: x => x.DespesasDespesaId,
                        principalTable: "Despesas",
                        principalColumn: "DespesaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormasPagamento",
                columns: table => new
                {
                    FormaPagamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaPagamentoNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasPagamento", x => x.FormaPagamentoId);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaParcelas",
                columns: table => new
                {
                    ReceitaParcelaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceitaValor = table.Column<double>(type: "float", nullable: false),
                    ReceitaParcelaDeletado = table.Column<bool>(type: "bit", nullable: false),
                    StatusDespesas = table.Column<int>(type: "int", nullable: false),
                    ReceitaId = table.Column<int>(type: "int", nullable: false),
                    ReceitasReceitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaParcelas", x => x.ReceitaParcelaId);
                    table.ForeignKey(
                        name: "FK_ReceitaParcelas_Receitas_ReceitasReceitaId",
                        column: x => x.ReceitasReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosOperacoesLog",
                columns: table => new
                {
                    UsuarioOperacaoLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperacaoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeController = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeMetodo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeOperacao = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Error = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuariosUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosOperacoesLog", x => x.UsuarioOperacaoLogId);
                    table.ForeignKey(
                        name: "FK_UsuariosOperacoesLog_Usuarios_UsuariosUsuarioId",
                        column: x => x.UsuariosUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriasCategoriaId",
                table: "Despesas",
                column: "CategoriasCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_FormasPagamentoFormaPagamentoId",
                table: "Despesas",
                column: "FormasPagamentoFormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas",
                column: "UsuariosUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_UsuariosUsuarioId",
                table: "Categorias",
                column: "UsuariosUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaParcelas_DespesasDespesaId",
                table: "DespesaParcelas",
                column: "DespesasDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaParcelas_ReceitasReceitaId",
                table: "ReceitaParcelas",
                column: "ReceitasReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosOperacoesLog_UsuariosUsuarioId",
                table: "UsuariosOperacoesLog",
                column: "UsuariosUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Usuarios_UsuariosUsuarioId",
                table: "Categorias",
                column: "UsuariosUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");

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
                name: "FK_Categorias_Usuarios_UsuariosUsuarioId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_FormasPagamento_FormasPagamentoFormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "DespesaParcelas");

            migrationBuilder.DropTable(
                name: "FormasPagamento");

            migrationBuilder.DropTable(
                name: "ReceitaParcelas");

            migrationBuilder.DropTable(
                name: "UsuariosOperacoesLog");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_FormasPagamentoFormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_UsuariosUsuarioId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ReceitaDataInclusao",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ReceitaDeletado",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "ReceitaQuantidadeParcelas",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "CategoriasCategoriaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesaDataVencimento",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesaDeletado",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesaQuantidadeParcelas",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "FormaPagamentoId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuariosUsuarioId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "CategoriaDeletado",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "UsuariosUsuarioId",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "ReceitaDataVencimento",
                table: "Receitas",
                newName: "ReceitaData");

            migrationBuilder.RenameColumn(
                name: "FormasPagamentoFormaPagamentoId",
                table: "Despesas",
                newName: "StatusDespesas");

            migrationBuilder.RenameColumn(
                name: "DespesasDataInclusao",
                table: "Despesas",
                newName: "DespesasData");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceitaDataFim",
                table: "Receitas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ReceitaValor",
                table: "Receitas",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DespesaValor",
                table: "Despesas",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DespesasDataFim",
                table: "Despesas",
                type: "datetime2",
                nullable: true);

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
    }
}
