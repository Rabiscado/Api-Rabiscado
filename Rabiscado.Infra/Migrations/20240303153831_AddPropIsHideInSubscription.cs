using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPropIsHideInSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHide",
                table: "Subscriptions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHide",
                table: "Subscriptions");
        }
    }
}
