using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lojaHemilly.Migrations
{
    /// <inheritdoc />
    public partial class retiradaprecoTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoTotal",
                table: "Vendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoTotal",
                table: "Vendas",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
