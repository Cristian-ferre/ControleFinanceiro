using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class alterandoReceitasQuantidadesMesesParaReceitaDataFim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceitaQuantidadeMeses",
                table: "Receitas");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceitaDataFim",
                table: "Receitas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceitaDataFim",
                table: "Receitas");

            migrationBuilder.AddColumn<int>(
                name: "ReceitaQuantidadeMeses",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
