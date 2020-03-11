using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class ModelsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Issue_IssueId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_WriterId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Article_ArticleId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Volume_VolumeId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_MemberId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Reply_Comment_CommentId",
                table: "Reply");

            migrationBuilder.DropForeignKey(
                name: "FK_Reply_AspNetUsers_OwnerId",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Reply_CommentId",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Reply_OwnerId",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Issue_VolumeId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ArticleId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Article_CategoryId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_IssueId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_WriterId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Reply");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Reply",
                newName: "CommentID");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Notification",
                newName: "MemberID");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_MemberId",
                table: "Notification",
                newName: "IX_Notification_MemberID");

            migrationBuilder.RenameColumn(
                name: "VolumeId",
                table: "Issue",
                newName: "VolumeID");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Comment",
                newName: "ArticleID");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Article",
                newName: "IssueID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Article",
                newName: "CategoryID");

            migrationBuilder.AlterColumn<int>(
                name: "CommentID",
                table: "Reply",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "Reply",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VolumeID",
                table: "Issue",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleID",
                table: "Comment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "Comment",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IssueID",
                table: "Article",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Article",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberID",
                table: "Article",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_MemberID",
                table: "Notification",
                column: "MemberID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_MemberID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Reply");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Reply",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "Notification",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_MemberID",
                table: "Notification",
                newName: "IX_Notification_MemberId");

            migrationBuilder.RenameColumn(
                name: "VolumeID",
                table: "Issue",
                newName: "VolumeId");

            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "Comment",
                newName: "ArticleId");

            migrationBuilder.RenameColumn(
                name: "IssueID",
                table: "Article",
                newName: "IssueId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Article",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Reply",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Reply",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VolumeId",
                table: "Issue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IssueId",
                table: "Article",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Article",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "WriterId",
                table: "Article",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reply_CommentId",
                table: "Reply",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_OwnerId",
                table: "Reply",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_VolumeId",
                table: "Issue",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArticleId",
                table: "Comment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryId",
                table: "Article",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_IssueId",
                table: "Article",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_WriterId",
                table: "Article",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Issue_IssueId",
                table: "Article",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_WriterId",
                table: "Article",
                column: "WriterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Article_ArticleId",
                table: "Comment",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Volume_VolumeId",
                table: "Issue",
                column: "VolumeId",
                principalTable: "Volume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_MemberId",
                table: "Notification",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_Comment_CommentId",
                table: "Reply",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_AspNetUsers_OwnerId",
                table: "Reply",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
