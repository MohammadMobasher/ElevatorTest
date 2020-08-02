using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_UserTreeReminded_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TreeRate",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tree",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    IsPlanted = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    TreeNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tree_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTreeReminded",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Reminded = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTreeReminded", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTreeReminded_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tree_UserId",
                table: "Tree",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTreeReminded_UserId",
                table: "UserTreeReminded",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tree");

            migrationBuilder.DropTable(
                name: "UserTreeReminded");

            migrationBuilder.DropColumn(
                name: "TreeRate",
                table: "SiteSetting");
        }
    }
}
