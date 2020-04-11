using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_OrderField_to_productGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProductGroup",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_PackageUserAnswers_FeatureId",
            //    table: "PackageUserAnswers",
            //    column: "FeatureId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PackageUserAnswers_Feature_FeatureId",
            //    table: "PackageUserAnswers",
            //    column: "FeatureId",
            //    principalTable: "Feature",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PackageUserAnswers_Feature_FeatureId",
            //    table: "PackageUserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_PackageUserAnswers_FeatureId",
                table: "PackageUserAnswers");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProductGroup");
        }
    }
}
