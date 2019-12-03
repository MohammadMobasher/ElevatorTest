using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisLike",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IndexPic",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductUnitId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Product",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Product",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Visit",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pic = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallery_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductUnitId",
                table: "Product",
                column: "ProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_ProductId",
                table: "Gallery",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductUnit_ProductUnitId",
                table: "Product",
                column: "ProductUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductUnit_ProductUnitId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductUnitId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DisLike",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IndexPic",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductUnitId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Visit",
                table: "Product");
        }
    }
}
