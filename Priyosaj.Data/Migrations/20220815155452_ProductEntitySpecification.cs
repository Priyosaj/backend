﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priyosaj.Data.Migrations
{
    public partial class ProductEntitySpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specification",
                table: "Products");
        }
    }
}
