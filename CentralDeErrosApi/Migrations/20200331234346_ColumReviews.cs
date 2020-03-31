using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErrosApi.Migrations
{
    public partial class ColumReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Error_Ambiente_AmbienteEnvironmentId",
                table: "Error");

            migrationBuilder.DropIndex(
                name: "IX_Error_AmbienteEnvironmentId",
                table: "Error");

            migrationBuilder.DropColumn(
                name: "AmbienteEnvironmentId",
                table: "Error");

            migrationBuilder.DropColumn(
                name: "EAmbiente_Id",
                table: "Error");

            migrationBuilder.AddColumn<int>(
                name: "AmbienteId",
                table: "Error",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ambiente_Id",
                table: "Error",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Error_AmbienteId",
                table: "Error",
                column: "AmbienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Error_Ambiente_AmbienteId",
                table: "Error",
                column: "AmbienteId",
                principalTable: "Ambiente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Error_Ambiente_AmbienteId",
                table: "Error");

            migrationBuilder.DropIndex(
                name: "IX_Error_AmbienteId",
                table: "Error");

            migrationBuilder.DropColumn(
                name: "AmbienteId",
                table: "Error");

            migrationBuilder.DropColumn(
                name: "Ambiente_Id",
                table: "Error");

            migrationBuilder.AddColumn<int>(
                name: "AmbienteEnvironmentId",
                table: "Error",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EAmbiente_Id",
                table: "Error",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Error_AmbienteEnvironmentId",
                table: "Error",
                column: "AmbienteEnvironmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Error_Ambiente_AmbienteEnvironmentId",
                table: "Error",
                column: "AmbienteEnvironmentId",
                principalTable: "Ambiente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
