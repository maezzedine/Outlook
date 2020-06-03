using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class Addarticlerelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryID",
                table: "Article",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_IssueID",
                table: "Article",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_MemberID",
                table: "Article",
                column: "MemberID");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Member_MemberID",
                table: "Article",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Article_CategoryID",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_IssueID",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_MemberID",
                table: "Article");
        }
    }
}
