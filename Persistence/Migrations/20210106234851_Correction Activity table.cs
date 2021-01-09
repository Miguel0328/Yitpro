using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CorrectionActivitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Activity",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Requirement",
                table: "Activity",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinalDate",
                table: "Activity",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Activity",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activity",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Activity",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "Activity",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Activity_UpdatedById",
                table: "Activity",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_User_UpdatedById",
                table: "Activity",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_User_UpdatedById",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam");

            migrationBuilder.DropIndex(
                name: "IX_Activity_UpdatedById",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Activity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Activity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<string>(
                name: "Requirement",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinalDate",
                table: "Activity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Activity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
