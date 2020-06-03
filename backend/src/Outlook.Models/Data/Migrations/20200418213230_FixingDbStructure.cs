using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class FixingDbStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Category_CategoryId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_CategoryId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Member");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_CategoryID",
                table: "CategoryEditor",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEditor_Category_CategoryID",
                table: "CategoryEditor",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEditor_Category_CategoryID",
                table: "CategoryEditor");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEditor_CategoryID",
                table: "CategoryEditor");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_CategoryId",
                table: "Member",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Category_CategoryId",
                table: "Member",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
