using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priyosaj.Data.Migrations
{
    public partial class FileEntityUpdateWithProductsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileEntityProduct",
                columns: table => new
                {
                    ImagesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntityProduct", x => new { x.ImagesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_FileEntityProduct_FileEntities_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "FileEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileEntityProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileEntityProduct_ProductsId",
                table: "FileEntityProduct",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileEntityProduct");
        }
    }
}
