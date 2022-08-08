using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priyosaj.Data.Migrations
{
    public partial class ProductEntityDisplayImageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DisplayImageId",
                table: "Products",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayImageId",
                table: "Products");
        }
    }
}
