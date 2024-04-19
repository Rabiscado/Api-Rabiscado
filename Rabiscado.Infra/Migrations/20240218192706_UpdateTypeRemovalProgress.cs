using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTypeRemovalProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseForWhos_Courses_CourseId",
                table: "CourseForWhos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseForWhos_ForWhos_ForWhoId",
                table: "CourseForWhos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLevels_Courses_CourseId",
                table: "CourseLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLevels_Levels_LevelId",
                table: "CourseLevels");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseForWhos_Courses_CourseId",
                table: "CourseForWhos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseForWhos_ForWhos_ForWhoId",
                table: "CourseForWhos",
                column: "ForWhoId",
                principalTable: "ForWhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLevels_Courses_CourseId",
                table: "CourseLevels",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLevels_Levels_LevelId",
                table: "CourseLevels",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseForWhos_Courses_CourseId",
                table: "CourseForWhos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseForWhos_ForWhos_ForWhoId",
                table: "CourseForWhos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLevels_Courses_CourseId",
                table: "CourseLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLevels_Levels_LevelId",
                table: "CourseLevels");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseForWhos_Courses_CourseId",
                table: "CourseForWhos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseForWhos_ForWhos_ForWhoId",
                table: "CourseForWhos",
                column: "ForWhoId",
                principalTable: "ForWhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLevels_Courses_CourseId",
                table: "CourseLevels",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLevels_Levels_LevelId",
                table: "CourseLevels",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
