using Microsoft.EntityFrameworkCore.Migrations;

namespace SysVentas.Products.Infrastructure.Data.Migrations
{
    public partial class addedProductPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                schema: "Productos",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Productos",
                table: "Products");
        }
    }
}
