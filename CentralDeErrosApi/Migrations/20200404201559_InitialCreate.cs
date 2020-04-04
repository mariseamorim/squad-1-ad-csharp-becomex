using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErrosApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Environment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Environment = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(maxLength: 105, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Situation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Situation = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Token = table.Column<string>(maxLength: 400, nullable: false),
                    EXPIRATION = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogErrorOccurrence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(maxLength: 200, nullable: false),
                    Details = table.Column<string>(maxLength: 2000, nullable: false),
                    Date_Time = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    SituationId = table.Column<int>(nullable: false),
                    CodeErro = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Environmente_Id = table.Column<int>(nullable: false),
                    EnvironmentId = table.Column<int>(nullable: false),
                    LevelId = table.Column<int>(nullable: false),
                    UsersUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErrorOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogErrorOccurrence_Environment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogErrorOccurrence_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogErrorOccurrence_Situation_SituationId",
                        column: x => x.SituationId,
                        principalTable: "Situation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogErrorOccurrence_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_EnvironmentId",
                table: "LogErrorOccurrence",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_LevelId",
                table: "LogErrorOccurrence",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_SituationId",
                table: "LogErrorOccurrence",
                column: "SituationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogErrorOccurrence_UsersUserId",
                table: "LogErrorOccurrence",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogErrorOccurrence");

            migrationBuilder.DropTable(
                name: "Environment");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "Situation");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
