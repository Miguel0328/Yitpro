using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DataContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GeneralCatalogMoldel_DepartmentId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_LevelModel_LevelId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserTypeModel_UserTypeId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTypeModel",
                table: "UserTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelModel",
                table: "LevelModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneralCatalogMoldel",
                table: "GeneralCatalogMoldel");

            migrationBuilder.RenameTable(
                name: "UserTypeModel",
                newName: "UserType");

            migrationBuilder.RenameTable(
                name: "LevelModel",
                newName: "Level");

            migrationBuilder.RenameTable(
                name: "GeneralCatalogMoldel",
                newName: "GeneralCatalog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserType",
                table: "UserType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level",
                table: "Level",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneralCatalog",
                table: "GeneralCatalog",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserTypePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(nullable: false),
                    UserTypeId = table.Column<int>(nullable: false),
                    Watch = table.Column<bool>(nullable: false),
                    Save = table.Column<bool>(nullable: false),
                    Modify = table.Column<bool>(nullable: false),
                    Print = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTypePermissions_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTypePermissions_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTypePermissions_MenuId",
                table: "UserTypePermissions",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypePermissions_UserTypeId",
                table: "UserTypePermissions",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GeneralCatalog_DepartmentId",
                table: "User",
                column: "DepartmentId",
                principalTable: "GeneralCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Level_LevelId",
                table: "User",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GeneralCatalog_DepartmentId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Level_LevelId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserType_UserTypeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "UserTypePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserType",
                table: "UserType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Level",
                table: "Level");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneralCatalog",
                table: "GeneralCatalog");

            migrationBuilder.RenameTable(
                name: "UserType",
                newName: "UserTypeModel");

            migrationBuilder.RenameTable(
                name: "Level",
                newName: "LevelModel");

            migrationBuilder.RenameTable(
                name: "GeneralCatalog",
                newName: "GeneralCatalogMoldel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTypeModel",
                table: "UserTypeModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelModel",
                table: "LevelModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneralCatalogMoldel",
                table: "GeneralCatalogMoldel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GeneralCatalogMoldel_DepartmentId",
                table: "User",
                column: "DepartmentId",
                principalTable: "GeneralCatalogMoldel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_LevelModel_LevelId",
                table: "User",
                column: "LevelId",
                principalTable: "LevelModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserTypeModel_UserTypeId",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserTypeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
