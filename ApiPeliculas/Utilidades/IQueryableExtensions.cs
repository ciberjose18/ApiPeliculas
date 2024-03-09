using ApiJuegos.DTOs;

namespace ApiJuegos.Utilidades
{
    public static class IQueryableExtensions
    {
        // Este es un método de extensión. El 'this' antes de IQueryable<T> significa que estamos añadiendo esta función a cualquier IQueryable<T>.
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDTO)
        {
            // Calculamos cuántos registros necesitamos omitir.
            // Si estamos en la página 1, no omitimos ninguno. Si estamos en la página 2, omitimos los registros de la página 1,
            // y así sucesivamente.
            //Por ejemplo Skip(10) omite los primeros 10 registros.
            //Take(5) toma los siguientes 5 registros.
            return queryable.Skip((paginacionDTO.Pagina - 1) * paginacionDTO.RecordsXpagina)
                .Take(paginacionDTO.RecordsXpagina);
        }
    }
}
