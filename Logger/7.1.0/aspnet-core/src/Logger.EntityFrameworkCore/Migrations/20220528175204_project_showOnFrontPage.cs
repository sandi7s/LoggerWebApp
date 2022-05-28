using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logger.Migrations
{
    public partial class project_showOnFrontPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowOnFrontPage",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnFrontPage",
                table: "Project");
        }
    }
}
