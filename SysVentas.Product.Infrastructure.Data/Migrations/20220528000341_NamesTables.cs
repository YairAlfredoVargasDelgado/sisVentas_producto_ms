using Microsoft.EntityFrameworkCore.Migrations;

namespace SysVentas.Products.Infrastructure.Data.Migrations
{
    public partial class NamesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                schema: "Productos",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                schema: "Productos",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "Productos",
                newName: "Products",
                newSchema: "Productos");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                schema: "Productos",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "Productos",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                schema: "Productos",
                table: "Products",
                column: "CategoryId",
                principalSchema: "Productos",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                schema: "Productos",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "Productos",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "Productos",
                newName: "Product",
                newSchema: "Productos");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                schema: "Productos",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                schema: "Productos",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                schema: "Productos",
                table: "Product",
                column: "CategoryId",
                principalSchema: "Productos",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
