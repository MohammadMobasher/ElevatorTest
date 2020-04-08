using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class Create_FeatureQuestionForPakage_table_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeatureQuestionForPakage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeatureId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    QuestionTitle = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureQuestionForPakage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureQuestionForPakage_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureQuestionForPakage_ProductGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureQuestionForPakage_FeatureId",
                table: "FeatureQuestionForPakage",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureQuestionForPakage_GroupId",
                table: "FeatureQuestionForPakage",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureQuestionForPakage");
        }
    }
}
