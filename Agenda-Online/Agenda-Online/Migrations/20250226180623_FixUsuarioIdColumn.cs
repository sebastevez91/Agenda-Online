using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda_Online.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar la columna incorrecta
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Users_IdUser",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Contact");

            // Crear la nueva columna con el tipo correcto (string)
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Contact",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Users_IdUser",
                table: "Contact",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
