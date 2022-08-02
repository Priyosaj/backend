using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priyosaj.Business.Data.Migrations
{
    public partial class UpdateProductCategoryTitleRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Title",
                table: "ProductCategories",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_Title",
                table: "ProductCategories");
        }
    }
}
