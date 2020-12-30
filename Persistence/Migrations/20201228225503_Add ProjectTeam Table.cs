using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddProjectTeamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTeam",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeam", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_ProjectId",
                table: "ProjectTeam",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_UpdatedById",
                table: "ProjectTeam",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_UserId",
                table: "ProjectTeam",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTeam");
        }
    }
}
