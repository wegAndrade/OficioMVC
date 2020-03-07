using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OficioMVC.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siga_profs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_login = table.Column<string>(nullable: true),
                    user_pass = table.Column<string>(nullable: true),
                    user_nicename = table.Column<string>(nullable: true),
                    ativo = table.Column<int>(nullable: false),
                    dpto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siga_profs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(nullable: true),
                    Observacoes = table.Column<string>(nullable: true),
                    CaminhoArq = table.Column<string>(nullable: true),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    UsuarioID = table.Column<int>(nullable: true),
                    Siga_profs_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Siga_profs_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Siga_profs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documento_UsuarioID",
                table: "Documento",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Siga_profs");
        }
    }
}
