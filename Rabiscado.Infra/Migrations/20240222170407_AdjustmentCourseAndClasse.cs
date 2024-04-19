using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentCourseAndClasse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Classes_ClassId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Steps");

            migrationBuilder.AddColumn<string>(
                name: "Gif",
                table: "Classes",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Classes_ClassId",
                table: "Steps",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Classes_ClassId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Gif",
                table: "Classes");

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Steps",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Steps",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Classes_ClassId",
                table: "Steps",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
