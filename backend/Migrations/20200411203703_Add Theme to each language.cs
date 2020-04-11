using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddThemetoeachlanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Issue");

            migrationBuilder.AddColumn<string>(
                name: "ArabicTheme",
                table: "Issue",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishTheme",
                table: "Issue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicTheme",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "EnglishTheme",
                table: "Issue");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
