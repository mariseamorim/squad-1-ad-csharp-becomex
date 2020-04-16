using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErrosApi.Migrations
{
    public partial class RevisaoFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogErrorOccurrence_Users_UsersUserId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropIndex(
                name: "IX_LogErrorOccurrence_UsersUserId",
                table: "LogErrorOccurrence");

            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "LogErrorOccurrence");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "LogErrorOccurrence",
                type: "int",
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
    }
}
