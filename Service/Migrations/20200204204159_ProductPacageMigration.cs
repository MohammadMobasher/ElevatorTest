using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductPacageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Discount = table.Column<long>(nullable: false),
                    StartDiscount = table.Column<DateTime>(nullable: false),
                    EndDiscount = table.Column<DateTime>(nullable: false),
                    ShortDiscription = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    IndexPic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackageDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPackageDetails_ProductPackage_PackageId",
                        column: x => x.PackageId,
                        principalTable: "ProductPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPackageDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackageGallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageId = table.Column<int>(nullable: false),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackageGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPackageGallery_ProductPackage_PackageId",
                        column: x => x.PackageId,
                        principalTable: "ProductPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPackageDetails_PackageId",
                table: "ProductPackageDetails",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPackageDetails_ProductId",
                table: "ProductPackageDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPackageGallery_PackageId",
                table: "ProductPackageGallery",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPackageDetails");

            migrationBuilder.DropTable(
                name: "ProductPackageGallery");

            migrationBuilder.DropTable(
                name: "ProductPackage");
        }
    }
}
