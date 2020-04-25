using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class Addissuerelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ar_pdf",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "en_pdf",
                table: "Issue");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_VolumeID",
                table: "Issue",
                column: "VolumeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Volume_VolumeID",
                table: "Issue",
                column: "VolumeID",
                principalTable: "Volume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Volume_VolumeID",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_VolumeID",
                table: "Issue");

            migrationBuilder.AddColumn<string>(
                name: "ar_pdf",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "en_pdf",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
