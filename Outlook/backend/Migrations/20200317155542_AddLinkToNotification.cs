using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddLinkToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArabicBoard_Member_MemberID",
                table: "ArabicBoard");

            migrationBuilder.DropForeignKey(
                name: "FK_EnglishBoard_Member_MemberID",
                table: "EnglishBoard");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Notification",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "EnglishBoard",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "ArabicBoard",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArabicBoard_Member_MemberID",
                table: "ArabicBoard",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnglishBoard_Member_MemberID",
                table: "EnglishBoard",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArabicBoard_Member_MemberID",
                table: "ArabicBoard");

            migrationBuilder.DropForeignKey(
                name: "FK_EnglishBoard_Member_MemberID",
                table: "EnglishBoard");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "EnglishBoard",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "ArabicBoard",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ArabicBoard_Member_MemberID",
                table: "ArabicBoard",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnglishBoard_Member_MemberID",
                table: "EnglishBoard",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
