using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductDiscountAddCreateDateFeild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductDiscount");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductDiscount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductDiscount");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductDiscount",
                nullable: true);
        }
    }
}
