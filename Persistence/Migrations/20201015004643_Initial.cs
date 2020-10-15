using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(nullable: false),
                    ShortDesc = table.Column<string>(nullable: true),
                    LongDesc = table.Column<string>(nullable: true),
                    SpecialData = table.Column<string>(nullable: true),
                    Header = table.Column<bool>(nullable: false),
                    Protected = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    IdUCreate = table.Column<long>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    IdUModified = table.Column<long>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(nullable: true),
                    ComplianceFactor = table.Column<decimal>(nullable: false),
                    HourFactor = table.Column<decimal>(nullable: false),
                    Standard = table.Column<decimal>(nullable: false),
                    DailyStandard = table.Column<decimal>(nullable: false),
                    WeekStandard = table.Column<decimal>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    IdUCreate = table.Column<long>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IdParent = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Order = table.Column<long>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Protected = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UserCreateId = table.Column<long>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    UserModifiedId = table.Column<long>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    AdmissionDate = table.Column<DateTime>(nullable: false),
                    LevelChangeDate = table.Column<DateTime>(nullable: false),
                    LevelId = table.Column<long>(nullable: true),
                    UserTypeId = table.Column<int>(nullable: true),
                    ManagerId = table.Column<long>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    BlockingAttempts = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Capture = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Locked = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: true),
                    DatePassword = table.Column<DateTime>(nullable: false),
                    UserCreateId = table.Column<long>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    UserModifiedId = table.Column<long>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_GeneralCatalog_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "GeneralCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "View",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    Controller = table.Column<string>(maxLength: 50, nullable: false),
                    View = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View", x => x.Id);
                    table.ForeignKey(
                        name: "FK_View_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LevelId",
                table: "User",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypePermissions_MenuId",
                table: "UserTypePermissions",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypePermissions_UserTypeId",
                table: "UserTypePermissions",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_View_UserId",
                table: "View",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTypePermissions");

            migrationBuilder.DropTable(
                name: "View");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "GeneralCatalog");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
