using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserPlanSubscriptionCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanSubscriptions_Users_UserId",
                table: "UserPlanSubscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanSubscriptions_Users_UserId",
                table: "UserPlanSubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanSubscriptions_Users_UserId",
                table: "UserPlanSubscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanSubscriptions_Users_UserId",
                table: "UserPlanSubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
