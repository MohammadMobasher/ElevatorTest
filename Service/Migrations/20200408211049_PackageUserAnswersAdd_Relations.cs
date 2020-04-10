using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class PackageUserAnswersAdd_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "PackageUserAnswers",
                newName: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageUserAnswers_QuestionId",
                table: "PackageUserAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageUserAnswers_FeatureQuestionForPakage_QuestionId",
                table: "PackageUserAnswers",
                column: "QuestionId",
                principalTable: "FeatureQuestionForPakage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageUserAnswers_FeatureQuestionForPakage_QuestionId",
                table: "PackageUserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_PackageUserAnswers_QuestionId",
                table: "PackageUserAnswers");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "PackageUserAnswers",
                newName: "FeatureId");
        }
    }
}
