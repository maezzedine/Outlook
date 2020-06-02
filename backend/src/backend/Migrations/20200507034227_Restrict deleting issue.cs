using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class Restrictdeletingissue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Issue_IssueID",
                table: "Article");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Issue_IssueID",
                table: "Article",
                column: "IssueID",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Issue_IssueID",
                table: "Article");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Issue_IssueID",
                table: "Article",
                column: "IssueID",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
