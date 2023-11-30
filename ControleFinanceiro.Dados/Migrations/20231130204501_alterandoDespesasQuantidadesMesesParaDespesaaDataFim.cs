using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class alterandoDespesasQuantidadesMesesParaDespesaaDataFim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DespesasQuantidadeMeses",
                table: "Despesas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DespesasDataFim",
                table: "Despesas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DespesasDataFim",
                table: "Despesas");

            migrationBuilder.AddColumn<int>(
                name: "DespesasQuantidadeMeses",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
