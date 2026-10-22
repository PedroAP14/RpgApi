using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUmParaUm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonagemId",
                table: "TB_ARMAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonagemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonagemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonagemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonagemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonagemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonagemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonagemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 75, 187, 193, 153, 97, 240, 31, 212, 70, 184, 174, 141, 11, 100, 168, 140, 83, 114, 160, 42, 28, 203, 212, 54, 187, 229, 193, 100, 22, 197, 127, 19, 40, 72, 157, 102, 123, 213, 13, 139, 191, 236, 105, 79, 204, 49, 139, 184, 201, 118, 243, 53, 124, 76, 245, 218, 82, 5, 179, 6, 52, 182, 217, 188 }, new byte[] { 231, 72, 151, 204, 10, 103, 236, 137, 133, 10, 83, 0, 87, 139, 230, 198, 101, 130, 155, 132, 215, 181, 77, 91, 152, 81, 147, 145, 212, 141, 119, 231, 56, 125, 242, 247, 197, 169, 20, 234, 178, 24, 85, 255, 158, 193, 233, 107, 144, 9, 254, 173, 149, 100, 218, 59, 12, 120, 178, 34, 176, 191, 79, 141, 159, 33, 124, 138, 150, 160, 253, 215, 48, 58, 161, 245, 54, 162, 74, 246, 161, 34, 168, 41, 113, 203, 5, 209, 245, 209, 60, 183, 22, 150, 113, 1, 214, 5, 210, 177, 147, 3, 72, 151, 44, 236, 245, 59, 240, 157, 39, 99, 3, 156, 186, 18, 227, 47, 106, 11, 196, 229, 61, 157, 63, 79, 74, 199 } });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                principalTable: "TB_PERSONAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 5, 2, 183, 132, 104, 57, 224, 189, 181, 238, 127, 135, 168, 195, 140, 142, 210, 82, 71, 114, 240, 110, 234, 72, 225, 191, 123, 91, 244, 101, 193, 245, 96, 122, 86, 210, 166, 195, 133, 180, 95, 140, 70, 40, 79, 155, 192, 184, 75, 59, 78, 29, 119, 237, 209, 209, 191, 71, 76, 28, 202, 191, 73, 228 }, new byte[] { 66, 177, 96, 124, 144, 241, 190, 15, 197, 28, 182, 144, 33, 72, 194, 189, 215, 64, 78, 137, 88, 215, 232, 213, 47, 213, 128, 245, 3, 82, 50, 216, 201, 92, 136, 205, 11, 131, 253, 165, 147, 67, 82, 103, 243, 237, 140, 6, 70, 44, 198, 237, 244, 138, 56, 99, 222, 73, 137, 234, 30, 23, 129, 246, 164, 94, 126, 242, 253, 242, 129, 63, 59, 149, 194, 91, 215, 222, 149, 255, 208, 57, 190, 54, 161, 216, 34, 62, 231, 245, 82, 80, 56, 46, 234, 43, 136, 203, 39, 61, 8, 2, 156, 111, 200, 241, 127, 251, 121, 231, 248, 25, 210, 72, 225, 225, 120, 9, 252, 251, 124, 110, 47, 142, 9, 199, 178, 82 } });
        }
    }
}
