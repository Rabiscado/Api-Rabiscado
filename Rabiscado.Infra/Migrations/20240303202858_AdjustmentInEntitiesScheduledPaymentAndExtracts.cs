using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentInEntitiesScheduledPaymentAndExtracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Scheduledpayments");

            migrationBuilder.DropColumn(
                name: "Professor",
                table: "Scheduledpayments");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Scheduledpayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Extracts",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Extracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scheduledpayments_CourseId",
                table: "Scheduledpayments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Extracts_CourseId",
                table: "Extracts",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extracts_Courses_CourseId",
                table: "Extracts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduledpayments_Courses_CourseId",
                table: "Scheduledpayments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extracts_Courses_CourseId",
                table: "Extracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduledpayments_Courses_CourseId",
                table: "Scheduledpayments");

            migrationBuilder.DropIndex(
                name: "IX_Scheduledpayments_CourseId",
                table: "Scheduledpayments");

            migrationBuilder.DropIndex(
                name: "IX_Extracts_CourseId",
                table: "Extracts");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Scheduledpayments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Extracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Scheduledpayments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Professor",
                table: "Scheduledpayments",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Extracts",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
