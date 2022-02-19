using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priyosaj.Business.Data.Migrations
{
    public partial class ProductCategoryUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductCategory");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductCategories");

            migrationBuilder.CreateTable(
                name: "ProductProductCategory",
                columns: table => new
                {
                    ProductCategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductCategory", x => new { x.ProductCategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_ProductCategories_ProductCategoriesId",
                        column: x => x.ProductCategoriesId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductCategory_ProductsId",
                table: "ProductProductCategory",
                column: "ProductsId");
        }
    }
}
