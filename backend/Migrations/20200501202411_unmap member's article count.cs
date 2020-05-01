using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class unmapmembersarticlecount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfArticles",
                table: "Member");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfArticles",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
