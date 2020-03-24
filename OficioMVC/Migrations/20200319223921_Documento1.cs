using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OficioMVC.Migrations
{
    public partial class Documento1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Documento");

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true),
                    CaminhoArq = table.Column<string>(nullable: true),
                    DataEnvio = table.Column<DateTime>(nullable: true),
                    UsuarioID = table.Column<int>(nullable: true),
                    Siga_profs_id = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: true)
                });

                       migrationBuilder.CreateIndex(
                name: "IX_Documento_Id_Ano",
                table: "Documento",
                columns: new[] { "Id", "Ano" },
                unique: true);
            }   

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Documento",
                nullable: false,
                oldClrType: typeof(int));

           

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Documento",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
               name: "Assunto",
               table: "Documento",
               type: "varchar(250)",
               nullable: false,
               oldClrType: typeof(string));


            migrationBuilder.AlterColumn<string>(
           name: "Observacoes",
           table: "Documento",
           type: "varchar(250)",
           nullable: false,
           oldClrType: typeof(string));
        }
    }
}
