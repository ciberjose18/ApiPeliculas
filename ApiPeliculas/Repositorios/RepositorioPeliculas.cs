using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace ApiJuegos.Repositorios
{
    public class RepositorioPeliculas : IRepositorioPeliculas
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<RepositorioPeliculas> logger;
        private readonly HttpContext? httpContext;

        public RepositorioPeliculas(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, 
            ILogger<RepositorioPeliculas> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            httpContext = httpContextAccessor.HttpContext;
            
        }

        // Método para obtener todos las peliculas con paginación

        public async Task<List<Pelicula>> ObtenerTodos(PaginacionDTO paginacionDTO)
        {
            var queryable = context.Peliculas.AsQueryable();

            await httpContext.InsertarParamPaginacionCabecera(queryable);

            return await queryable.OrderBy(a => a.Id).Paginar(paginacionDTO).ToListAsync();
        }

        public async Task<Pelicula?> ObtenerXid(int id)
        {
            //Esto carga los comentarios relacionados con cada película. Esto se llama carga ansiosa,
            //que significa que los datos relacionados se cargan desde la base de datos como parte de la consulta inicial.
            return await context.Peliculas.Include(p => p.Comentarios)
                .Include(p=> p.GeneroPeliculas)                   // Aquí, estamos cargando los géneros de las películas. Primero, cargamos GeneroPeliculas,
                .ThenInclude(gp => gp.Genero)                     //  que es probablemente una tabla de unión entre Peliculas y Generos. Luego, con ThenInclude, cargamos los Generos asociados a cada GeneroPelicula.
                .Include(p => p.ActoresPeliculas.OrderBy(ap => ap.Orden))
                .ThenInclude(ap => ap.Actor)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            //Esto le dice a Entity Framework que no rastree ninguna de las entidades que se devuelven en el resultado de la consulta.
            //Esto puede ser útil si sabes que no necesitarás hacer ninguna operación de actualización en las entidades devueltas.
        }

        public async Task<int> Crear(Pelicula pelicula) 
        {
            context.Add(pelicula);
            await context.SaveChangesAsync();
            return pelicula.Id;
        }
        public async Task Actualizar(Pelicula pelicula)
        {

            context.Update(pelicula);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {

            await context.Peliculas.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> Existe(int id)
        {

            return await context.Peliculas.AnyAsync(p => p.Id == id);
        }

        // Toma como parámetros el ID de la película y una lista de IDs de géneros que se van a asignar a la película.
        public async Task AsignarGeneros(int id, List<int> generosId)
        {
            // Aquí obtenemos la película de la base de datos utilizando su ID.
            // Utilizamos 'Include' para cargar la relación GeneroPeliculas asociada a la película.
            var peliculas = await context.Peliculas.Include(p => p.GeneroPeliculas).FirstOrDefaultAsync(x => x.Id == id);

            // Verificamos si la película existe (si no es null).
            if (peliculas is null)
            {
                throw new ArgumentException($"No exite una pelicula con el id: {id}");
            }

            // Se crea una lista de objetos GeneroPelicula a partir de la lista de IDs de género
            // Utilizamos 'Select' para transformar cada ID de género en un nuevo objeto GeneroPelicula con el GeneroId correspondiente.
            var generosPeliculas = generosId.Select(generosId => new GeneroPelicula() { GeneroId = generosId });
            // Se actualiza la lista de géneros de la película usando AutoMapper
            peliculas.GeneroPeliculas = mapper.Map(generosPeliculas, peliculas.GeneroPeliculas);
            
            await context.SaveChangesAsync();   
        }

        public async Task AsignarActores(int id, List<ActorPelicula> actorPeliculas)
        {
            // Aquí tenemos un 'for'. Va a contar desde 1 hasta el número de elementos en la lista 'actorPeliculas'.
            // En cada paso del bucle, vamos a asignar un número (orden) a cada ActorPelicula en la lista.
            for (int i = 1; i <= actorPeliculas.Count; i++)
            {
                // Estamos tomando el ActorPelicula en la posición 'i-1' (porque la lista comienza en la posición 0),
                // y le estamos asignando el valor de 'i' a su propiedad 'Orden'.
                actorPeliculas[i-1].Orden = i;
            }

            var pelicula = await context.Peliculas.Include(p => p.ActoresPeliculas).FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula is null)
            {
                throw new ArgumentException($"No exite una pelicula con el id: {id}");
            }

            pelicula.ActoresPeliculas = mapper.Map(actorPeliculas, pelicula.ActoresPeliculas);

            await context.SaveChangesAsync();
        }

        public async Task<List<Pelicula>> FiltrarPeliculas(PeliculasFiltrarDTO filtrarDTO)
        {
            var peliculasQueryable = context.Peliculas.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(filtrarDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Titulo.Contains(filtrarDTO.Titulo));
            }

            // Comprobamos si el valor de 'EnCines' tiene un valor
            if (filtrarDTO.EnCines.HasValue)
            {
                //Si 'EnCines' es verdadero, filtramos la lista de películas para incluir solo las que están en cines
                if (filtrarDTO.EnCines.Value)
                {
                    peliculasQueryable = peliculasQueryable.Where(p => p.EnCines);
                }
                // Si 'EnCines' es falso, filtramos la lista de películas para incluir solo las que no están en cines
                else
                {
                    peliculasQueryable = peliculasQueryable.Where(p => !p.EnCines);
                }
            }

            if(filtrarDTO.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(p => p.FechaLanzamiento > hoy);
            }

            if (filtrarDTO.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.GeneroPeliculas.Select(gp => gp.GeneroId)
                .Contains(filtrarDTO.GeneroId));
            }
            //Apartado ordenar
            //Este fragmento de código es parte de una función que se utiliza para ordenar una lista de películas.
            //Tenemos una caja llena de películas y quieremos ordenarlos de alguna manera,
            //por ejemplo, por el título de la película o por la fecha de lanzamiento. Eso es exactamente lo que hace este código.
            if (!string.IsNullOrWhiteSpace(filtrarDTO.CampoOrdenar))
            {
                var tipoOrden = filtrarDTO.OrdenAscendente ? "ascending" : "descending";
                try
                {
                    peliculasQueryable = peliculasQueryable
                        .OrderBy($"{filtrarDTO.CampoOrdenar} {tipoOrden}");
                }
                catch (Exception ex) 
                {
                    logger.LogError(ex.Message, ex);
                }
            }

            await httpContext.InsertarParamPaginacionCabecera(peliculasQueryable);
            var peliculas = await peliculasQueryable.Paginar(filtrarDTO.PaginacionDTO).ToListAsync();
            return peliculas;
        }
    }
}
