using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductAddSpecialFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductPackage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DisLike",
                table: "ProductPackage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductPackage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "ProductPackage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Visit",
                table: "ProductPackage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductPackage");

            migrationBuilder.DropColumn(
                name: "DisLike",
                table: "ProductPackage");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductPackage");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "ProductPackage");

            migrationBuilder.DropColumn(
                name: "Visit",
                table: "ProductPackage");
        }
    }
}
