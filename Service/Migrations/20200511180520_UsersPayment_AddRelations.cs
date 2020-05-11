using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UsersPayment_AddRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPayment_AspNetUsers_UsersId",
                table: "UsersPayment");

            migrationBuilder.DropIndex(
                name: "IX_UsersPayment_UsersId",
                table: "UsersPayment");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "UsersPayment");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "UsersPayment");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessed",
                table: "UsersPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPayment_UserId",
                table: "UsersPayment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPayment_AspNetUsers_UserId",
                table: "UsersPayment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPayment_AspNetUsers_UserId",
                table: "UsersPayment");

            migrationBuilder.DropIndex(
                name: "IX_UsersPayment_UserId",
                table: "UsersPayment");

            migrationBuilder.DropColumn(
                name: "IsSuccessed",
                table: "UsersPayment");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "UsersPayment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "UsersPayment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPayment_UsersId",
                table: "UsersPayment",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPayment_AspNetUsers_UsersId",
                table: "UsersPayment",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
