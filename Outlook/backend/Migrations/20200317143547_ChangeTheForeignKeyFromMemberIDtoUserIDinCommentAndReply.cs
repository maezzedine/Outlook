using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class ChangeTheForeignKeyFromMemberIDtoUserIDinCommentAndReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Reply");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Reply",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reply");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "Reply",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
