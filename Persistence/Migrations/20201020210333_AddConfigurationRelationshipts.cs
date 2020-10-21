using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddConfigurationRelationshipts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_CreatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ManagerId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Role_CreatedById",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UpdatedById",
                table: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatedById",
                table: "Role",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedById",
                table: "Role",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_CreatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ManagerId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Role_CreatedById",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UpdatedById",
                table: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById",
                unique: true,
                filter: "[CreatedById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById",
                unique: true,
                filter: "[UpdatedById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatedById",
                table: "Role",
                column: "CreatedById",
                unique: true,
                filter: "[CreatedById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedById",
                table: "Role",
                column: "UpdatedById",
                unique: true,
                filter: "[UpdatedById] IS NOT NULL");
        }
    }
}
