using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddCatalogColumnsToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MethodologyId",
                table: "Project",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "PSP",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "Project",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Project",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Phase",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Catalog",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Catalog_MethodologyId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Catalog_StatusId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Catalog_TypeId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_MethodologyId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_StatusId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TypeId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "MethodologyId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PSP",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Project");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Phase",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Catalog",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");
        }
    }
}
