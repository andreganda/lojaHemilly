using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lojaHemilly.Migrations
{
    /// <inheritdoc />
    public partial class inclusaodetipopagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoPagamento",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "TipoPagamento",
                table: "Vendas");
        }
    }
}
