using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoAtributosEntitiesDespesasEReceitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Receitas",
                newName: "ReceitaData");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Despesas",
                newName: "DespesasData");

            migrationBuilder.AddColumn<int>(
                name: "ReceitaQuantidadeMeses",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DespesasQuantidadeMeses",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceitaQuantidadeMeses",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "DespesasQuantidadeMeses",
                table: "Despesas");

            migrationBuilder.RenameColumn(
                name: "ReceitaData",
                table: "Receitas",
                newName: "DataInicio");

            migrationBuilder.RenameColumn(
                name: "DespesasData",
                table: "Despesas",
                newName: "DataInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Receitas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Despesas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
