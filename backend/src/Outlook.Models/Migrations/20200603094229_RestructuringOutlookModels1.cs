using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class RestructuringOutlookModels1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Category_CategoryID",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Issue_IssueID",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Member_MemberID",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Volume_VolumeID",
                table: "Issue");

            migrationBuilder.DropTable(
                name: "UserFavoritedArticleRelation");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Volume_VolumeNumber",
                table: "Volume");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Issue_VolumeID_IssueNumber",
                table: "Issue");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Category_CategoryName",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Article_MemberID",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "VolumeNumber",
                table: "Volume");

            migrationBuilder.DropColumn(
                name: "IssueNumber",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserRateArticle",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Member",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VolumeID",
                table: "Issue",
                newName: "VolumeId");

            migrationBuilder.RenameColumn(
                name: "IssueID",
                table: "Article",
                newName: "IssueId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Article",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_IssueID",
                table: "Article",
                newName: "IX_Article_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_CategoryID",
                table: "Article",
                newName: "IX_Article_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Volume",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VolumeId",
                table: "Issue",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Issue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Category",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Category",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Category",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Article",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IssueId",
                table: "Article",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Article",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Article",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Volume_Number",
                table: "Volume",
                column: "Number");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Category_Name",
                table: "Category",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "UserFavoriteArticle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ArticleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavoriteArticle_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteArticle_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issue_VolumeId",
                table: "Issue",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_WriterId",
                table: "Article",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteArticle_ArticleId",
                table: "UserFavoriteArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteArticle_UserId",
                table: "UserFavoriteArticle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Issue_IssueId",
                table: "Article",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Member_WriterId",
                table: "Article",
                column: "WriterId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Volume_VolumeId",
                table: "Issue",
                column: "VolumeId",
                principalTable: "Volume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Issue_IssueId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Member_WriterId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Volume_VolumeId",
                table: "Issue");

            migrationBuilder.DropTable(
                name: "UserFavoriteArticle");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Volume_Number",
                table: "Volume");

            migrationBuilder.DropIndex(
                name: "IX_Issue_VolumeId",
                table: "Issue");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Category_Name",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Article_WriterId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Volume");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserRateArticle",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Member",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "VolumeId",
                table: "Issue",
                newName: "VolumeID");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Article",
                newName: "IssueID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Article",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Article_IssueId",
                table: "Article",
                newName: "IX_Article_IssueID");

            migrationBuilder.RenameIndex(
                name: "IX_Article_CategoryId",
                table: "Article",
                newName: "IX_Article_CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "VolumeNumber",
                table: "Volume",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VolumeID",
                table: "Issue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssueNumber",
                table: "Issue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Tag",
                table: "Category",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "Category",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "Article",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IssueID",
                table: "Article",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Article",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Volume_VolumeNumber",
                table: "Volume",
                column: "VolumeNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Issue_VolumeID_IssueNumber",
                table: "Issue",
                columns: new[] { "VolumeID", "IssueNumber" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Category_CategoryName",
                table: "Category",
                column: "CategoryName");

            migrationBuilder.CreateTable(
                name: "UserFavoritedArticleRelation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoritedArticleRelation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserFavoritedArticleRelation_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoritedArticleRelation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_MemberID",
                table: "Article",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoritedArticleRelation_ArticleId",
                table: "UserFavoritedArticleRelation",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoritedArticleRelation_UserId",
                table: "UserFavoritedArticleRelation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Category_CategoryID",
                table: "Article",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Issue_IssueID",
                table: "Article",
                column: "IssueID",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Member_MemberID",
                table: "Article",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Volume_VolumeID",
                table: "Issue",
                column: "VolumeID",
                principalTable: "Volume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
