using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Userpermissionstablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissionsModel_Menu_MenuId",
                table: "UserPermissionsModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissionsModel_User_UpdatedById",
                table: "UserPermissionsModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissionsModel_User_UserId",
                table: "UserPermissionsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionsModel",
                table: "UserPermissionsModel");

            migrationBuilder.RenameTable(
                name: "UserPermissionsModel",
                newName: "UserPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissionsModel_UserId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissionsModel_UpdatedById",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UpdatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                columns: new[] { "MenuId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Menu_MenuId",
                table: "UserPermissions",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_User_UpdatedById",
                table: "UserPermissions",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_User_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Menu_MenuId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_User_UpdatedById",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_User_UserId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                newName: "UserPermissionsModel");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissionsModel",
                newName: "IX_UserPermissionsModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UpdatedById",
                table: "UserPermissionsModel",
                newName: "IX_UserPermissionsModel_UpdatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionsModel",
                table: "UserPermissionsModel",
                columns: new[] { "MenuId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissionsModel_Menu_MenuId",
                table: "UserPermissionsModel",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissionsModel_User_UpdatedById",
                table: "UserPermissionsModel",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissionsModel_User_UserId",
                table: "UserPermissionsModel",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
