using Microsoft.EntityFrameworkCore.Migrations;

namespace OficioMVC.Migrations
{
    public partial class IDDocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documento_Id_Ano",
                table: "Documento");

            migrationBuilder.AddColumn<int>(
                name: "Numeracao",
                table: "Documento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Numeracao_Ano",
                table: "Documento",
                columns: new[] { "Numeracao", "Ano" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documento_Numeracao_Ano",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Numeracao",
                table: "Documento");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Id_Ano",
                table: "Documento",
                columns: new[] { "Id", "Ano" },
                unique: true);
        }
    }
}
