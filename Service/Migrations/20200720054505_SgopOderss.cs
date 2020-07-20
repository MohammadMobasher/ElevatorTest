using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class SgopOderss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "UsersPayment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuccessDate",
                table: "ShopOrderPayment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "UsersPayment");

            migrationBuilder.DropColumn(
                name: "SuccessDate",
                table: "ShopOrderPayment");
        }
    }
}
