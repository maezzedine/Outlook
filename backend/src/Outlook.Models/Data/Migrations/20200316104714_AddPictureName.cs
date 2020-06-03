using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class AddPictureName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Article",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Article");
        }
    }
}
