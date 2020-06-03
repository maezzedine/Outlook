using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class AddUserLikeAndDislikeArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDislikeArticle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    ArticleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDislikeArticle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserLikeArticle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    ArticleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikeArticle", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDislikeArticle");

            migrationBuilder.DropTable(
                name: "UserLikeArticle");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
