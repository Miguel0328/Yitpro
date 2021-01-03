using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ProjectTeamkeychanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTeam",
                table: "ProjectTeam");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTeam_UserId",
                table: "ProjectTeam");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectTeam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTeam",
                table: "ProjectTeam",
                columns: new[] { "UserId", "ProjectId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTeam",
                table: "ProjectTeam");

            migrationBuilder.AddColumn<short>(
                name: "Id",
                table: "ProjectTeam",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTeam",
                table: "ProjectTeam",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_UserId",
                table: "ProjectTeam",
                column: "UserId");
        }
    }
}
