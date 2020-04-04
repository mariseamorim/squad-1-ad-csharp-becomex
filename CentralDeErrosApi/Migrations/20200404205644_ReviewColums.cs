using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErrosApi.Migrations
{
    public partial class ReviewColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogErrorOccurrence_Environment_EnvironmentId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropForeignKey(
                name: "FK_LogErrorOccurrence_Level_LevelId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropIndex(
                name: "IX_LogErrorOccurrence_EnvironmentId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropIndex(
                name: "IX_LogErrorOccurrence_LevelId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropColumn(
                name: "EnvironmentId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LogErrorOccurrence");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnvironmentId",
                table: "LogErrorOccurrence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LogErrorOccurrence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_EnvironmentId",
                table: "LogErrorOccurrence",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_LevelId",
                table: "LogErrorOccurrence",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogErrorOccurrence_Environment_EnvironmentId",
                table: "LogErrorOccurrence",
                column: "EnvironmentId",
                principalTable: "Environment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogErrorOccurrence_Level_LevelId",
                table: "LogErrorOccurrence",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
