using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Server.Migrations
{
    public partial class CategoryEditorRelationadjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor");

            migrationBuilder.DropColumn(
                name: "NumberOfFavorites",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "NumberOfVotes",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor",
                column: "MemberID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfFavorites",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVotes",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor",
                column: "MemberID");
        }
    }
}
