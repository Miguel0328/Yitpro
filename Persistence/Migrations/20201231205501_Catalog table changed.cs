using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Catalogtablechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Catalog_CatalogId1",
                table: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Catalog_CatalogId1",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "CatalogId1",
                table: "Catalog");

            migrationBuilder.AlterColumn<long>(
                name: "CatalogId",
                table: "Catalog",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogId",
                table: "Catalog",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Catalog_CatalogId",
                table: "Catalog",
                column: "CatalogId",
                principalTable: "Catalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Catalog_CatalogId",
                table: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Catalog_CatalogId",
                table: "Catalog");

            migrationBuilder.AlterColumn<short>(
                name: "CatalogId",
                table: "Catalog",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CatalogId1",
                table: "Catalog",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogId1",
                table: "Catalog",
                column: "CatalogId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Catalog_CatalogId1",
                table: "Catalog",
                column: "CatalogId1",
                principalTable: "Catalog",
                principalColumn: "Id");
        }
    }
}
