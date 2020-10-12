using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdmissionDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BlockingAttempts",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Capture",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "User",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePassword",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "User",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "LevelChangeDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "LevelId",
                table: "User",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                table: "User",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreateId",
                table: "User",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserModifiedId",
                table: "User",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GeneralCatalogMoldel",
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
                    table.PrimaryKey("PK_GeneralCatalogMoldel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelModel",
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
                    table.PrimaryKey("PK_LevelModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdParentMenu = table.Column<int>(nullable: false),
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
                name: "UserTypeModel",
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
                    table.PrimaryKey("PK_UserTypeModel", x => x.Id);
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
                name: "IX_User_ManagerId1",
                table: "User",
                column: "ManagerId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");

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
                name: "FK_User_User_ManagerId1",
                table: "User",
                column: "ManagerId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserTypeModel_UserTypeId",
                table: "User",
                column: "UserTypeId",
                principalTable: "UserTypeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GeneralCatalogMoldel_DepartmentId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_LevelModel_LevelId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_ManagerId1",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserTypeModel_UserTypeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "GeneralCatalogMoldel");

            migrationBuilder.DropTable(
                name: "LevelModel");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "UserTypeModel");

            migrationBuilder.DropIndex(
                name: "IX_User_DepartmentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_LevelId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ManagerId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserTypeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AdmissionDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BlockingAttempts",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Capture",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DatePassword",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LevelChangeDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "User");
        }
    }
}
