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
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(nullable: false),
                    AssignedId = table.Column<long>(nullable: false),
                    ResponsibleId = table.Column<long>(nullable: false),
                    PhaseId = table.Column<long>(nullable: false),
                    ClasificationId = table.Column<long>(nullable: false),
                    AssignedHours = table.Column<int>(nullable: false),
                    EstimatedHours = table.Column<int>(nullable: false),
                    FinalHours = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FinalDate = table.Column<DateTime>(nullable: false),
                    Requirement = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Critical = table.Column<bool>(nullable: false),
                    Planned = table.Column<bool>(nullable: false),
                    Urgent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phase",
                columns: table => new
                {
                    PhaseId = table.Column<long>(nullable: false),
                    ClasificationId = table.Column<long>(nullable: false),
                    PSP = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => new { x.PhaseId, x.ClasificationId });
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 1200, nullable: false),
                    ClientId = table.Column<short>(nullable: false),
                    LeaderId = table.Column<long>(nullable: false),
                    TypeId = table.Column<long>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    MethodologyId = table.Column<long>(nullable: false),
                    PSP = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    SecondLastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    RoleId = table.Column<short>(nullable: false),
                    ManagerId = table.Column<long>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    Capture = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Locked = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 300, nullable: true),
                    PasswordLastUpdate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Photo = table.Column<string>(nullable: true),
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
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Alias = table.Column<string>(maxLength: 100, nullable: false),
                    Protected = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_Catalog_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catalog_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeam",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeam", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectTeam_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectTeam_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    MenuId = table.Column<short>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Access = table.Column<bool>(nullable: false),
                    Create = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => new { x.MenuId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserPermission_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermission_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    MenuId = table.Column<short>(nullable: false),
                    RoleId = table.Column<short>(nullable: false),
                    Access = table.Column<bool>(nullable: false),
                    Create = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_AssignedId",
                table: "Activity",
                column: "AssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ClasificationId",
                table: "Activity",
                column: "ClasificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_PhaseId",
                table: "Activity",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ProjectId",
                table: "Activity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ResponsibleId",
                table: "Activity",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogId",
                table: "Catalog",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_UpdatedById",
                table: "Catalog",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UpdatedById",
                table: "Client",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Phase_ClasificationId",
                table: "Phase",
                column: "ClasificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phase_UpdatedById",
                table: "Phase",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LeaderId",
                table: "Project",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_MethodologyId",
                table: "Project",
                column: "MethodologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StatusId",
                table: "Project",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TypeId",
                table: "Project",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UpdatedById",
                table: "Project",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_ProjectId",
                table: "ProjectTeam",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_UpdatedById",
                table: "ProjectTeam",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UpdatedById",
                table: "Role",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_UpdatedById",
                table: "RolePermission",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UpdatedById",
                table: "UserPermission",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermission",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_User_AssignedId",
                table: "Activity",
                column: "AssignedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_User_ResponsibleId",
                table: "Activity",
                column: "ResponsibleId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Catalog_ClasificationId",
                table: "Activity",
                column: "ClasificationId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Catalog_PhaseId",
                table: "Activity",
                column: "PhaseId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Project_ProjectId",
                table: "Activity",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_User_UpdatedById",
                table: "Phase",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Catalog_ClasificationId",
                table: "Phase",
                column: "ClasificationId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Catalog_PhaseId",
                table: "Phase",
                column: "PhaseId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_LeaderId",
                table: "Project",
                column: "LeaderId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UpdatedById",
                table: "Project",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Catalog_MethodologyId",
                table: "Project",
                column: "MethodologyId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Catalog_StatusId",
                table: "Project",
                column: "StatusId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Catalog_TypeId",
                table: "Project",
                column: "TypeId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Catalog_DepartmentId",
                table: "User",
                column: "DepartmentId",
                principalTable: "Catalog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_User_UpdatedById",
                table: "Catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UpdatedById",
                table: "Role");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Phase");

            migrationBuilder.DropTable(
                name: "ProjectTeam");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
