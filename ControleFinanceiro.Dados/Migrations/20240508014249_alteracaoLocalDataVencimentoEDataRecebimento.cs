using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoLocalDataVencimentoEDataRecebimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceitaDataVencimento",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "DespesaDataVencimento",
                table: "Despesas");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceitaDataRecebimento",
                table: "ReceitaParcelas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DespesaDataVencimento",
                table: "DespesaParcelas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceitaDataRecebimento",
                table: "ReceitaParcelas");

            migrationBuilder.DropColumn(
                name: "DespesaDataVencimento",
                table: "DespesaParcelas");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceitaDataVencimento",
                table: "Receitas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DespesaDataVencimento",
                table: "Despesas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
