using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class addStatusReceitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusDespesas",
                table: "ReceitaParcelas",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusDespesas",
                table: "DespesaParcelas",
                newName: "Status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceitaDataRecebimento",
                table: "ReceitaParcelas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ReceitaParcelas",
                newName: "StatusDespesas");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DespesaParcelas",
                newName: "StatusDespesas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceitaDataRecebimento",
                table: "ReceitaParcelas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
