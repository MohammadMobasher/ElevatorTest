using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class d1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDependencies_Condition_ConditionId",
                table: "ProductDependencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDependencies",
                table: "ProductDependencies");

            migrationBuilder.RenameTable(
                name: "ProductDependencies",
                newName: "ProductGroupDependencies");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDependencies_ConditionId",
                table: "ProductGroupDependencies",
                newName: "IX_ProductGroupDependencies_ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroupDependencies",
                table: "ProductGroupDependencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroupDependencies_Condition_ConditionId",
                table: "ProductGroupDependencies",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroupDependencies_Condition_ConditionId",
                table: "ProductGroupDependencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroupDependencies",
                table: "ProductGroupDependencies");

            migrationBuilder.RenameTable(
                name: "ProductGroupDependencies",
                newName: "ProductDependencies");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroupDependencies_ConditionId",
                table: "ProductDependencies",
                newName: "IX_ProductDependencies_ConditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDependencies",
                table: "ProductDependencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDependencies_Condition_ConditionId",
                table: "ProductDependencies",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
