using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class AddrelationbetweenRatedArticlesandarticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserRateArticle",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "UserRateArticle",
                newName: "ArticleId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRateArticle",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "UserRateArticle",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserRateArticle_ArticleId",
                table: "UserRateArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRateArticle_UserId",
                table: "UserRateArticle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRateArticle_Article_ArticleId",
                table: "UserRateArticle",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRateArticle_AspNetUsers_UserId",
                table: "UserRateArticle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRateArticle_Article_ArticleId",
                table: "UserRateArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRateArticle_AspNetUsers_UserId",
                table: "UserRateArticle");

            migrationBuilder.DropIndex(
                name: "IX_UserRateArticle_ArticleId",
                table: "UserRateArticle");

            migrationBuilder.DropIndex(
                name: "IX_UserRateArticle_UserId",
                table: "UserRateArticle");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRateArticle",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "UserRateArticle",
                newName: "ArticleID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserRateArticle",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleID",
                table: "UserRateArticle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
