using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErrosApi.Migrations
{
    public partial class AddReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogErrorOccurrence_Users_USER_ID",
                table: "LogErrorOccurrence");

            migrationBuilder.DropIndex(
                name: "IX_LogErrorOccurrence_USER_ID",
                table: "LogErrorOccurrence");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "LogErrorOccurrence");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "LogErrorOccurrence",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_UsersUserId",
                table: "LogErrorOccurrence",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogErrorOccurrence_Users_UsersUserId",
                table: "LogErrorOccurrence",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogErrorOccurrence_Users_UsersUserId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropIndex(
                name: "IX_LogErrorOccurrence_UsersUserId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "LogErrorOccurrence");

            migrationBuilder.AddColumn<int>(
                name: "USER_ID",
                table: "LogErrorOccurrence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_USER_ID",
                table: "LogErrorOccurrence",
                column: "USER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LogErrorOccurrence_Users_USER_ID",
                table: "LogErrorOccurrence",
                column: "USER_ID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
