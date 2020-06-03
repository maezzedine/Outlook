using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class AddCategoryTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tag",
                table: "Category",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Category");
        }
    }
}
