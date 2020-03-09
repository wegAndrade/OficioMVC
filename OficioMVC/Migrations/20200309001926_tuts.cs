using Microsoft.EntityFrameworkCore.Migrations;

namespace OficioMVC.Migrations
{
    public partial class tuts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "dpto",
                table: "Siga_profs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ativo",
                table: "Siga_profs",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "dpto",
                table: "Siga_profs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ativo",
                table: "Siga_profs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
