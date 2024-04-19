using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var senha =
                "$argon2id$v=19$m=32768,t=4,p=1$8kSN61J8u9f2fBanH2sbjA$mcjis6H1GOwjNVVNBznVkOkktsa+CHUc9bP95x8IsEo";
            
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Email", "Password", "Cpf", "Phone", "Cep", "Coin", "CreateAt", "UpdateAt", "IsAdmin", "IsProfessor" },
                values: new object[,]
                {
                    { 1, "Admin"    , "admin@admin.com"        , senha, "58818787012", "41999366266", "69316028", 0, "2024-02-17 19:05:48", "2024-02-17 19:05:48", true, true  },
                    { 2, "Professor", "professor@professor.com", senha, "10451586077", "83989827923", "91360000", 0, "2024-02-17 19:05:48", "2024-02-17 19:05:48", false, true  },
                    { 3, "Common"   , "common@common.com"      , senha, "09795750011", "95994309086", "91150092", 0, "2024-02-17 19:05:48", "2024-02-17 19:05:48", false, false  },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Users", "Id", 1);
            migrationBuilder.DeleteData("Users", "Id", 2);
            migrationBuilder.DeleteData("Users", "Id", 3);
        }
    }
}