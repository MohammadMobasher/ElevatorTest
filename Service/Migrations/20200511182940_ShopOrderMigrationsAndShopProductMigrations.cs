using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ShopOrderMigrationsAndShopProductMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopOrderId",
                table: "ShopProduct",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShopOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<long>(nullable: false),
                    IsSuccessed = table.Column<bool>(nullable: false),
                    SuccessDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DiscountCode = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopOrder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_ShopOrderId",
                table: "ShopProduct",
                column: "ShopOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_UserId",
                table: "ShopOrder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_ShopOrder_ShopOrderId",
                table: "ShopProduct",
                column: "ShopOrderId",
                principalTable: "ShopOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_ShopOrder_ShopOrderId",
                table: "ShopProduct");

            migrationBuilder.DropTable(
                name: "ShopOrder");

            migrationBuilder.DropIndex(
                name: "IX_ShopProduct_ShopOrderId",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "ShopOrderId",
                table: "ShopProduct");
        }
    }
}
