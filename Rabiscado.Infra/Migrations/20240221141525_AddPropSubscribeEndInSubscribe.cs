using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPropSubscribeEndInSubscribe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Subscriptions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEnd",
                table: "Subscriptions",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionEnd",
                table: "Subscriptions");
        }
    }
}
