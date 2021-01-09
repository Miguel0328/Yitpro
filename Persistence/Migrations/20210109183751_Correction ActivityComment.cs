using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CorrectionActivityComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityComment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Comment = table.Column<string>(maxLength: 3000, nullable: true),
                    Log = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityComment_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityComment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityComment_ActivityId",
                table: "ActivityComment",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityComment_UserId",
                table: "ActivityComment",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityComment");
        }
    }
}
