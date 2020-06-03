using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class UpdatePictureToPicturePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Article",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
