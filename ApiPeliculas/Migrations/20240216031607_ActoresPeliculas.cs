﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiJuegos.Migrations
{
    /// <inheritdoc />
    public partial class ActoresPeliculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActoresPeliculas",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Pesonaje = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActoresPeliculas", x => new { x.ActorId, x.PeliculaId });
                    table.ForeignKey(
                        name: "FK_ActoresPeliculas_Actores_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActoresPeliculas_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActoresPeliculas_PeliculaId",
                table: "ActoresPeliculas",
                column: "PeliculaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActoresPeliculas");
        }
    }
}
