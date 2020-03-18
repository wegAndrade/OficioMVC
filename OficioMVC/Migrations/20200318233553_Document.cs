using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OficioMVC.Migrations
{
    public partial class Document : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Siga_profs_UsuarioID",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Documento",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Documento_UsuarioID",
                table: "Documento",
                newName: "IX_Documento_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "user_pass",
                table: "Siga_profs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_login",
                table: "Siga_profs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Documento",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Assunto",
                table: "Documento",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Documento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Documento",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Documento",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Siga_profs_UsuarioId",
                table: "Documento",
                column: "UsuarioId",
                principalTable: "Siga_profs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Siga_profs_UsuarioId",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Documento",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Documento_UsuarioId",
                table: "Documento",
                newName: "IX_Documento_UsuarioID");

            migrationBuilder.AlterColumn<string>(
                name: "user_pass",
                table: "Siga_profs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "user_login",
                table: "Siga_profs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Documento",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Assunto",
                table: "Documento",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Siga_profs_UsuarioID",
                table: "Documento",
                column: "UsuarioID",
                principalTable: "Siga_profs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
