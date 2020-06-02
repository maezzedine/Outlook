using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Server.Migrations
{
    public partial class OnDeleteArticleCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                table: "UserFavoritedArticleRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoritedArticleRelation_AspNetUsers_UserId",
                table: "UserFavoritedArticleRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRateArticle_Article_ArticleId",
                table: "UserRateArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRateArticle_AspNetUsers_UserId",
                table: "UserRateArticle");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                table: "UserFavoritedArticleRelation",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoritedArticleRelation_AspNetUsers_UserId",
                table: "UserFavoritedArticleRelation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRateArticle_Article_ArticleId",
                table: "UserRateArticle",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRateArticle_AspNetUsers_UserId",
                table: "UserRateArticle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                table: "UserFavoritedArticleRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoritedArticleRelation_AspNetUsers_UserId",
                table: "UserFavoritedArticleRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRateArticle_Article_ArticleId",
                table: "UserRateArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRateArticle_AspNetUsers_UserId",
                table: "UserRateArticle");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                table: "UserFavoritedArticleRelation",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoritedArticleRelation_AspNetUsers_UserId",
                table: "UserFavoritedArticleRelation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
