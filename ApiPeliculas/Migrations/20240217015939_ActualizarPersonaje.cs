using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiJuegos.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarPersonaje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pesonaje",
                table: "ActoresPeliculas",
                newName: "Personaje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Personaje",
                table: "ActoresPeliculas",
                newName: "Pesonaje");
        }
    }
}
