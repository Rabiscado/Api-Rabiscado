using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultValueForWho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ForWhos",
                columns: new[] { "Id", "Name", "Description", "CreateAt", "UpdateAt" },
                values: new object[,]
                {
                    { 1, "Casal", "Curso focado no casal", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  },
                    { 2, "Condutor", "Curso focado para o condutor", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  },
                    { 3, "Conduzido", "Curso focado para o conduzido", "2024-02-17 19:05:48", "2024-02-17 19:05:48"  }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ForWhos", "Id", 1);
            migrationBuilder.DeleteData("ForWhos", "Id", 2);
            migrationBuilder.DeleteData("ForWhos", "Id", 3);
        }
    }
}
