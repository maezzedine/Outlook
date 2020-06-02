using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Server.Migrations
{
    public partial class AddRelationbetweencommentandarticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArticleID",
                table: "Comment",
                column: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Article_ArticleID",
                table: "Comment",
                column: "ArticleID",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Article_ArticleID",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ArticleID",
                table: "Comment");
        }
    }
}
