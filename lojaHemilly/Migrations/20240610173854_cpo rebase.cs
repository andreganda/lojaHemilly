using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lojaHemilly.Migrations
{
    /// <inheritdoc />
    public partial class cporebase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Entrada",
                table: "Vendas",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entrada",
                table: "Vendas");
        }
    }
}
