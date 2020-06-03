using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class AddrelationbetweenFavoritedArticlesandarticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "UserFavoritedArticleRelation",
                newName: "ArticleId");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "UserFavoritedArticleRelation",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoritedArticleRelation_ArticleId",
                table: "UserFavoritedArticleRelation",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                table: "UserFavoritedArticleRelation",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                table: "UserFavoritedArticleRelation");

            migrationBuilder.DropIndex(
                name: "IX_UserFavoritedArticleRelation_ArticleId",
                table: "UserFavoritedArticleRelation");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "UserFavoritedArticleRelation",
                newName: "ArticleID");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleID",
                table: "UserFavoritedArticleRelation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
