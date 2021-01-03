using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPhaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phase",
                columns: table => new
                {
                    PhaseId = table.Column<long>(nullable: false),
                    ClasificationId = table.Column<long>(nullable: false),
                    PSP = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => new { x.PhaseId, x.ClasificationId });
                    table.ForeignKey(
                        name: "FK_Phase_Catalog_ClasificationId",
                        column: x => x.ClasificationId,
                        principalTable: "Catalog",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phase_Catalog_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Catalog",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phase_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phase_ClasificationId",
                table: "Phase",
                column: "ClasificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Phase_UpdatedById",
                table: "Phase",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phase");
        }
    }
}
