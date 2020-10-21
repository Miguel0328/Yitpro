using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddUserCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById",
                unique: true,
                filter: "[CreatedById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById",
                unique: true,
                filter: "[UpdatedById] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedById",
                table: "User",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UpdatedById",
                table: "User",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedById",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UpdatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedById",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdatedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
