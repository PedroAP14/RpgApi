using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoMuitosParaMuitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_HABILIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_HABILIDADES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PERSONAGENS_HABILIDADES",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PERSONAGENS_HABILIDADES", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_HABILIDADES_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "TB_HABILIDADES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TB_PERSONAGENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_HABILIDADES",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 37, "Hipnotizar" }
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 182, 91, 7, 247, 217, 10, 96, 199, 157, 237, 11, 147, 61, 111, 49, 95, 15, 207, 105, 42, 156, 72, 58, 95, 174, 214, 144, 130, 208, 198, 3, 196, 108, 102, 75, 234, 201, 150, 151, 221, 158, 58, 85, 12, 60, 117, 101, 166, 218, 196, 47, 178, 215, 154, 19, 37, 85, 238, 186, 15, 252, 240, 102, 23 }, new byte[] { 13, 20, 2, 60, 50, 48, 20, 229, 177, 127, 81, 192, 252, 42, 221, 251, 203, 66, 94, 166, 159, 118, 211, 68, 198, 52, 180, 224, 204, 138, 77, 152, 8, 206, 248, 200, 116, 76, 227, 94, 205, 211, 43, 40, 157, 186, 168, 51, 232, 8, 103, 215, 132, 55, 233, 89, 138, 53, 44, 77, 179, 205, 133, 136, 30, 154, 179, 37, 57, 158, 59, 143, 116, 175, 223, 93, 167, 89, 100, 208, 199, 76, 202, 39, 11, 59, 29, 164, 231, 38, 180, 231, 22, 71, 63, 153, 173, 119, 97, 244, 158, 120, 85, 2, 240, 58, 99, 206, 152, 73, 52, 191, 19, 196, 170, 97, 182, 117, 37, 225, 117, 15, 131, 122, 89, 181, 92, 206 } });

            migrationBuilder.InsertData(
                table: "TB_PERSONAGENS_HABILIDADES",
                columns: new[] { "HabilidadeId", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGENS_HABILIDADES_HabilidadeId",
                table: "TB_PERSONAGENS_HABILIDADES",
                column: "HabilidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PERSONAGENS_HABILIDADES");

            migrationBuilder.DropTable(
                name: "TB_HABILIDADES");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 75, 187, 193, 153, 97, 240, 31, 212, 70, 184, 174, 141, 11, 100, 168, 140, 83, 114, 160, 42, 28, 203, 212, 54, 187, 229, 193, 100, 22, 197, 127, 19, 40, 72, 157, 102, 123, 213, 13, 139, 191, 236, 105, 79, 204, 49, 139, 184, 201, 118, 243, 53, 124, 76, 245, 218, 82, 5, 179, 6, 52, 182, 217, 188 }, new byte[] { 231, 72, 151, 204, 10, 103, 236, 137, 133, 10, 83, 0, 87, 139, 230, 198, 101, 130, 155, 132, 215, 181, 77, 91, 152, 81, 147, 145, 212, 141, 119, 231, 56, 125, 242, 247, 197, 169, 20, 234, 178, 24, 85, 255, 158, 193, 233, 107, 144, 9, 254, 173, 149, 100, 218, 59, 12, 120, 178, 34, 176, 191, 79, 141, 159, 33, 124, 138, 150, 160, 253, 215, 48, 58, 161, 245, 54, 162, 74, 246, 161, 34, 168, 41, 113, 203, 5, 209, 245, 209, 60, 183, 22, 150, 113, 1, 214, 5, 210, 177, 147, 3, 72, 151, 44, 236, 245, 59, 240, 157, 39, 99, 3, 156, 186, 18, 227, 47, 106, 11, 196, 229, 61, 157, 63, 79, 74, 199 } });
        }
    }
}
