using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    ParentId = table.Column<short>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Route = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Order = table.Column<short>(nullable: false),
                    Level = table.Column<byte>(nullable: false),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    MenuId = table.Column<short>(nullable: false),
                    RoleId = table.Column<short>(nullable: false),
                    Access = table.Column<bool>(nullable: false),
                    Create = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    AdmissionDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<short>(nullable: true),
                    ManagerId = table.Column<long>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Locked = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: true),
                    PasswordLastUpdate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Protected = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedById",
                table: "Role",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_UpdatedById",
                table: "RolePermissions",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId1",
                table: "User",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_User_UpdatedById",
                table: "RolePermissions",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Role_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId1",
                table: "User",
                column: "RoleId1",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UpdatedById",
                table: "Role");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
