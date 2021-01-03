using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddCatalogColumnsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Capture",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Catalog_DepartmentId",
                table: "User",
                column: "DepartmentId",
                principalTable: "Catalog",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Catalog_DepartmentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DepartmentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Capture",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "User");
        }
    }
}
