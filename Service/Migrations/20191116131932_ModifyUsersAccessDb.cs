using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ModifyUsersAccessDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersAccess_RoleId",
                table: "UsersAccess",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAccess_AspNetRoles_RoleId",
                table: "UsersAccess",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAccess_AspNetRoles_RoleId",
                table: "UsersAccess");

            migrationBuilder.DropIndex(
                name: "IX_UsersAccess_RoleId",
                table: "UsersAccess");
        }
    }
}
