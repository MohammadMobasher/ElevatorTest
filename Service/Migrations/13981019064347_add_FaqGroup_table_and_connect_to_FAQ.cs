using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_FaqGroup_table_and_connect_to_FAQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaqGroupId",
                table: "FAQ",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FaqGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_FaqGroupId",
                table: "FAQ",
                column: "FaqGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_FaqGroup_FaqGroupId",
                table: "FAQ",
                column: "FaqGroupId",
                principalTable: "FaqGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_FaqGroup_FaqGroupId",
                table: "FAQ");

            migrationBuilder.DropTable(
                name: "FaqGroup");

            migrationBuilder.DropIndex(
                name: "IX_FAQ_FaqGroupId",
                table: "FAQ");

            migrationBuilder.DropColumn(
                name: "FaqGroupId",
                table: "FAQ");
        }
    }
}
