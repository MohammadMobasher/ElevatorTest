using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductDiscountModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "ProductDiscount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ProductDiscount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "ProductDiscount",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductDiscount",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ProductDiscount",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_ProductGroupId",
                table: "ProductDiscount",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_ProductId",
                table: "ProductDiscount",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_ProductGroup_ProductGroupId",
                table: "ProductDiscount",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscount_Product_ProductId",
                table: "ProductDiscount",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_ProductGroup_ProductGroupId",
                table: "ProductDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscount_Product_ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscount_ProductGroupId",
                table: "ProductDiscount");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscount_ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "ProductDiscount");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProductDiscount");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "ProductDiscount");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDiscount");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProductDiscount");
        }
    }
}
