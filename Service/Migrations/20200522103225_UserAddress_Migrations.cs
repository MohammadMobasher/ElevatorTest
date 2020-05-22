using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UserAddress_Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ShopOrderDetails");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ShopOrderDetails");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "ShopOrderDetails");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ShopOrderDetails");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ShopOrderDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ShopOrderDetails");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "ShopOrderDetails",
                newName: "FullName");

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Plaque = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_UserId",
                table: "UserAddress",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "ShopOrderDetails",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ShopOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ShopOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "ShopOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ShopOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ShopOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ShopOrderDetails",
                nullable: true);
        }
    }
}
