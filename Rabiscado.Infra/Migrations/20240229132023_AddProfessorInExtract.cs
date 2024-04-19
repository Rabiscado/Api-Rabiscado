using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessorInExtract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Extracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Extracts_ProfessorId",
                table: "Extracts",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extracts_Users_ProfessorId",
                table: "Extracts",
                column: "ProfessorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extracts_Users_ProfessorId",
                table: "Extracts");

            migrationBuilder.DropIndex(
                name: "IX_Extracts_ProfessorId",
                table: "Extracts");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Extracts");
        }
    }
}
