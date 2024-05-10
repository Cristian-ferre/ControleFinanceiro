using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    /// <inheritdoc />
    public partial class addNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_FormasPagamento_FormaPagamentoId",
                table: "Despesas");

            migrationBuilder.AlterColumn<int>(
                name: "FormaPagamentoId",
                table: "Despesas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_FormasPagamento_FormaPagamentoId",
                table: "Despesas",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "FormaPagamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_FormasPagamento_FormaPagamentoId",
                table: "Despesas");

            migrationBuilder.AlterColumn<int>(
                name: "FormaPagamentoId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_FormasPagamento_FormaPagamentoId",
                table: "Despesas",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "FormaPagamentoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
