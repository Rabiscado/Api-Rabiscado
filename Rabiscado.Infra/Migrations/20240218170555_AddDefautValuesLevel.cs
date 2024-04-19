using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDefautValuesLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Name", "Description", "CreateAt", "UpdateAt" },
                values: new object[,]
                {
                    { 1, "Iniciante", "Curso para pessoas que nunca fizeram aulas de dança", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  },
                    { 2, "Intermediário", "Curso para pessoas que tem experiência prévia com dança", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  },
                    { 3, "Avançado", "Curso para pessoas experientes em dança", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Levels", "Id", 1);
            migrationBuilder.DeleteData("Levels", "Id", 2);
            migrationBuilder.DeleteData("Levels", "Id", 3);
        }
    }
}
