using Microsoft.EntityFrameworkCore.Migrations;

namespace Outlook.Server.Migrations
{
    public partial class CategoryMemberDirectRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEditor");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Member",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CategoryEditor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEditor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CategoryEditor_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEditor_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_CategoryID",
                table: "CategoryEditor",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEditor_MemberID",
                table: "CategoryEditor",
                column: "MemberID",
                unique: true);
        }
    }
}
