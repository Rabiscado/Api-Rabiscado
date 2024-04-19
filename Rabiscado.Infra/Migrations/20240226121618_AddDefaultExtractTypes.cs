using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultExtractTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExtractTypes",
                columns: new[] { "Id", "Name", "CreateAt", "UpdateAt" },
                values: new object[,]
                {
                    { 1, "Entrada", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  },
                    { 2, "Saída", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ExtractTypes", "Id", 1);
            migrationBuilder.DeleteData("ExtractTypes", "Id", 2);
        }
    }
}
