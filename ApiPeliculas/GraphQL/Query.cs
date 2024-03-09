using ApiJuegos;
using ApiJuegos.Entidades;

namespace ApiPeliculas.GraphQL
{
    public class Query
    {
        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Genero> ObternerGeneros([Service] ApplicationDbContext context) => context.Generos;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Actor> ObternerActores([Service] ApplicationDbContext context) => context.Actores;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Pelicula> ObternerPeliculas([Service] ApplicationDbContext context) => context.Peliculas;
    }
}
