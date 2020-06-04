using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Models.Data.Migrations
{
    public partial class AddObjecctskeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Issue_VolumeID",
                table: "Issue");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Volume_VolumeNumber",
                table: "Volume",
                column: "VolumeNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Member_Name",
                table: "Member",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Issue_VolumeID_IssueNumber",
                table: "Issue",
                columns: new[] { "VolumeID", "IssueNumber" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Category_CategoryName",
                table: "Category",
                column: "CategoryName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Volume_VolumeNumber",
                table: "Volume");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Member_Name",
                table: "Member");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Issue_VolumeID_IssueNumber",
                table: "Issue");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Category_CategoryName",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Member",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Issue_VolumeID",
                table: "Issue",
                column: "VolumeID");
        }
    }
}
