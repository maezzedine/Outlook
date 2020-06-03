using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class MemberUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Member",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "CategoryEditor",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_CategoryId",
                table: "Member",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Category_CategoryId",
                table: "Member",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Category_CategoryId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_CategoryId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "CategoryEditor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
