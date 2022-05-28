using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logger.Migrations
{
    public partial class projects_full_audited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Project",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Project",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Project",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Project",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Project",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Project");
        }
    }
}
