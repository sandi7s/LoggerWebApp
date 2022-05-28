using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logger.Migrations
{
    public partial class logs_projectLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_ProjectId",
                table: "LogEntry",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogEntry_Project_ProjectId",
                table: "LogEntry",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogEntry_Project_ProjectId",
                table: "LogEntry");

            migrationBuilder.DropIndex(
                name: "IX_LogEntry_ProjectId",
                table: "LogEntry");
        }
    }
}
