using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Server.Migrations
{
    public partial class RemoveIssueCoverPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ar_cover",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "en_cover",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Article");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ar_cover",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "en_cover",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
