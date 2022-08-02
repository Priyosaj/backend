using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priyosaj.Business.Data.Migrations
{
    public partial class PromotionalEventMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionalEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    StartingTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndingTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionalEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionalEventProductMappings",
                columns: table => new
                {
                    PromotionalEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventDiscountPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionalEventProductMappings", x => new { x.PromotionalEventId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_PromotionalEventProductMappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionalEventProductMappings_PromotionalEvents_Promotion~",
                        column: x => x.PromotionalEventId,
                        principalTable: "PromotionalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionalEventProductMappings_ProductId",
                table: "PromotionalEventProductMappings",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionalEventProductMappings");

            migrationBuilder.DropTable(
                name: "PromotionalEvents");
        }
    }
}
