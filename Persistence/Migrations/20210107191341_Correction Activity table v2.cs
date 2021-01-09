using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CorrectionActivitytablev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam");

            migrationBuilder.DropColumn(
                name: "AssignedHours",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "EstimatedHours",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "FinalHours",
                table: "Activity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Activity",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedTime",
                table: "Activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedTime",
                table: "Activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalTime",
                table: "Activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "StatusId",
                table: "Activity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam");

            migrationBuilder.DropColumn(
                name: "AssignedTime",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "FinalTime",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Activity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Activity",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AddColumn<int>(
                name: "AssignedHours",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedHours",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalHours",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_User_UserId",
                table: "ProjectTeam",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
