using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OficioMVC.Migrations
{
    public partial class testes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siga_profs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_login = table.Column<string>(nullable: false),
                    user_pass = table.Column<string>(nullable: false),
                    user_nicename = table.Column<string>(nullable: true),
                    ativo = table.Column<string>(nullable: true),
                    dpto = table.Column<string>(nullable: true)
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
                    Numeracao = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Assunto = table.Column<string>(type: "varchar(2500)", nullable: false),
                    Observacoes =  table.Column<string>(type: "varchar(2500)", nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    CaminhoArq = table.Column<string>(nullable: true),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Siga_profs_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Siga_profs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documento_UsuarioId",
                table: "Documento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Numeracao_Ano",
                table: "Documento",
                columns: new[] { "Numeracao", "Ano" },
                unique: true);
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
