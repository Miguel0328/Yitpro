using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Catalogtableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<short>(nullable: true),
                    CatalogId1 = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Alias = table.Column<string>(maxLength: 100, nullable: false),
                    Protected = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_Catalog_CatalogId1",
                        column: x => x.CatalogId1,
                        principalTable: "Catalog",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Catalog_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogId1",
                table: "Catalog",
                column: "CatalogId1");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_UpdatedById",
                table: "Catalog",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");
        }
    }
}
