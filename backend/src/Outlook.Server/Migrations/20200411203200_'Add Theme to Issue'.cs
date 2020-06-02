using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Server.Migrations
{
    public partial class AddThemetoIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Issue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Issue");
        }
    }
}
