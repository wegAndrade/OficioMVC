using Microsoft.EntityFrameworkCore.Migrations;

namespace OficioMVC.Migrations
{
    public partial class constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Siga_profs_id",
                table: "Documento");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Id_Ano",
                table: "Documento",
                columns: new[] { "Id", "Ano" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documento_Id_Ano",
                table: "Documento");

            migrationBuilder.AddColumn<int>(
                name: "Siga_profs_id",
                table: "Documento",
                nullable: false,
                defaultValue: 0);
        }
    }
}
