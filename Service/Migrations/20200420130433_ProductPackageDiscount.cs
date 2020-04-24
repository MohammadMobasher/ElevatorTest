using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductPackageDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPackageDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discount = table.Column<long>(nullable: false),
                    PackageId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    DiscountType = table.Column<int>(nullable: false),
                    IsArchive = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackageDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPackageDiscount_ProductPackage_PackageId",
                        column: x => x.PackageId,
                        principalTable: "ProductPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPackageDiscount_PackageId",
                table: "ProductPackageDiscount",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPackageDiscount");
        }
    }
}
