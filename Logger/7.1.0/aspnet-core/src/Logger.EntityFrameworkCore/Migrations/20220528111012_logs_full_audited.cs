using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logger.Migrations
{
    public partial class logs_full_audited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "LogEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "LogEntry",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "LogEntry",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "LogEntry",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LogEntry",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "LogEntry",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "LogEntry",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "LogEntry");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "LogEntry");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "LogEntry");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "LogEntry");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LogEntry");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "LogEntry");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "LogEntry");
        }
    }
}
