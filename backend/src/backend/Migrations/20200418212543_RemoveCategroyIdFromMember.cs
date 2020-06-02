using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class RemoveCategroyIdFromMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEditor_Member_MemberID",
                table: "CategoryEditor",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
