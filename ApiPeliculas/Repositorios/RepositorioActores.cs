using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace ApiJuegos.Repositorios
{
    public class RepositorioActores : IRepositorioActores
    {
        private readonly ApplicationDbContext context;
        private readonly HttpContext? httpContext;

        public RepositorioActores(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext;
        }

        // Método para obtener todos los actores con paginación
        public async Task<List<Actor>> ObtenerTodos(PaginacionDTO paginacionDTO)
        {
            // Obtiene un IQueryable de actores a partir del contexto
            var queryable = context.Actores.AsQueryable();
            // Llama al método de extensión para insertar el parámetro de paginación en la cabecera de la respuesta HTTP
            await httpContext.InsertarParamPaginacionCabecera(queryable);
            // Retorna la lista de actores ordenada por el Id de forma asíncrona
            return await queryable.OrderBy(a => a.Id).Paginar(paginacionDTO).ToListAsync();
        }

        public async Task<Actor?> ObtenerXid(int id)
        {
            // La función AsNoTracking() indica que no se debe rastrear el estado de las entidades recuperadas,
            // lo que puede mejorar el rendimiento cuando solo se realiza una lectura de datos.
            // FirstOrDefaultAsync() busca el primer actor que coincida con la condición proporcionada (id).
            return await context.Actores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        // Método para obtener una lista de actores cuyos nombres contienen una cadena dada
        public async Task<List<Actor>> ObtenerXnombre(string nombre)
        {
            // Consulta la base de datos para obtener los actores cuyos nombres contienen la cadena especificada
            // Utiliza Entity Framework para realizar la consulta de manera asincrónica
            // y convierte el resultado en una lista antes de devolverla
            return await context.Actores.Where(a => a.Nombre.Contains(nombre)).ToListAsync();
        }

        public async Task<int> Crear(Actor actor)
        {
            context.Add(actor);
            await context.SaveChangesAsync();
            return actor.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Actores.AnyAsync(x => x.Id == id);
        }

        public async Task<List<int>> Existen(List<int> ids)
        {
            // Buscamos en la entidad 'Actores'
            // Queremos encontrar registros (filas) donde el valor en la columna 'Id' coincida con algún número de nuestra lista 'ids'.
            // Así que estamos filtrando la tabla 'Actores' para obtener solo las filas que tienen 'Id' en nuestra lista 'ids'.
            // En este caso, solo queremos la columna 'Id' de los actores que coinciden.

            return await context.Actores.Where(a => ids.Contains(a.Id)).Select(a => a.Id).ToListAsync();
        }

        public async Task Actualizar(Actor actor)
        {
            context.Update(actor);
            await context.SaveChangesAsync();
        }
        public async Task Borrar(int id)
        {
            await context.Actores.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

    }
}
