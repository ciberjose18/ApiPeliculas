using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Filtros;
using ApiJuegos.Migrations;
using ApiJuegos.Repositorios;
using ApiJuegos.Servicios;
using ApiJuegos.Utilidades;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System;
using System.Threading.Tasks;

namespace ApiJuegos.Endpoints
{
    public static class PeliculasEndpoints
    {

        private static readonly string carpeta = "peliculas";
        public static RouteGroupBuilder MapPeliculas(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerPelis).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("peliculas-get"))
                .AgregarParametrosPaginacionAOpenAPI();
            group.MapGet("/{id:int}", ObternerXid);
            group.MapPost("/", Crear).AddEndpointFilter<FiltroValidaciones<CrearPeliculaDTO>>().DisableAntiforgery()
                .RequireAuthorization("admin").WithOpenApi();
            group.MapPost("/{id:int}/asignargeneros", AsignarGeneros).DisableAntiforgery().RequireAuthorization("admin");
            group.MapPost("/{id:int}/asignaractores", AsignarActores).DisableAntiforgery().RequireAuthorization("admin");
            group.MapPut("/{id:int}", Actualizar).AddEndpointFilter<FiltroValidaciones<CrearPeliculaDTO>>().DisableAntiforgery()
                .RequireAuthorization("admin").WithOpenApi();
            group.MapDelete("/{id:int}", Borrar).RequireAuthorization("admin");

            group.MapGet("/filtrar", FiltrarPeliculas).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("peliculas-get"))
                .AgregarParametrosPeliculasFiltroAOpenAPI();

            return group;
        }

        static async Task<Created<PeliculaDTO>> Crear([FromForm] CrearPeliculaDTO crearPeliculaDTO,
            IRepositorioPeliculas repositorio, IAlmacenarArchivos almacenarArchivos, IOutputCacheStore outputCacheStore,
            IMapper mapper)
        {
            var pelicula = mapper.Map<Pelicula>(crearPeliculaDTO);

            if (crearPeliculaDTO.Poster is not null)
            {
                var url = await almacenarArchivos.Almacenar(carpeta, crearPeliculaDTO.Poster);
                pelicula.Poster = url;
            }

            var id = await repositorio.Crear(pelicula);
            await outputCacheStore.EvictByTagAsync("peliculas-get", default);
            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);

            return TypedResults.Created($"/peliculas/{id}", peliculaDTO);

        }

        static async Task<Ok<List<PeliculaDTO>>> ObtenerPelis(IRepositorioPeliculas repositorio, IMapper mapper,
            PaginacionDTO paginacion)
        {
            //var paginacion = new PaginacionDTO { Pagina = pagina, RecordsXpagina = recordsXpagina };
            var peliculas = await repositorio.ObtenerTodos(paginacion);
            var peliculaDTO = mapper.Map<List<PeliculaDTO>>(peliculas);

            return TypedResults.Ok(peliculaDTO);
        }

        // Este es un método que busca una película por su ID
        static async Task<Results<Ok<PeliculaDTO>, NotFound>> ObternerXid(int id, IRepositorioPeliculas repositorio, IMapper mapper)
        {
            // Aquí le pedimos al repositorio que busque una película con el ID dado
            var pelicula = await repositorio.ObtenerXid(id);

            // Si la película no se encuentra (es decir, es null), devolvemos un resultado NotFound
            if (pelicula is null)
            {
                return TypedResults.NotFound();
            }
            // Si la película se encuentra, la convertimos a un PeliculaDTO usando el mapper
            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            // Finalmente, devolvemos el PeliculaDTO con un resultado Ok
            return TypedResults.Ok(peliculaDTO);
        }

        static async Task<Results<NoContent, NotFound>> Actualizar(int id, [FromForm] CrearPeliculaDTO crearPeliculaDTO,
            IRepositorioPeliculas repositorio, IAlmacenarArchivos almacenarArchivos, IOutputCacheStore outputCacheStore,
            IMapper mapper)
        {

            var peliculaAntigua = await repositorio.ObtenerXid(id);
            if (peliculaAntigua is null) { return TypedResults.NotFound(); }

            // Mapear los datos del DTO a un objeto Pelicula

            var peliculaActualizar = mapper.Map<Pelicula>(crearPeliculaDTO);
            // Asignar el ID al peliculaActualizar
            peliculaActualizar.Id = id;
            // Mantener la foto existente de la pelicula para evitar cambios si no se proporciona una nueva

            peliculaActualizar.Poster = peliculaAntigua.Poster;

            if (crearPeliculaDTO.Poster is not null)
            {
                // Almacenar la nueva foto y obtener la URL
                var url = await almacenarArchivos.Editar(peliculaActualizar.Poster, carpeta, crearPeliculaDTO.Poster);

                // Actualizar la propiedad Foto del actor con la nueva URL

                peliculaActualizar.Poster = url;
            }
            // Actualizar la pelicula en la base de datos
            await repositorio.Actualizar(peliculaActualizar);
            // Invalidar la caché relacionada con peliculas
            await outputCacheStore.EvictByTagAsync("peliculas-get", default);
            // Devolver un resultado NoContent indicando que la actualización fue exitosa
            return TypedResults.NoContent();

        }

        static async Task<Results<NoContent, NotFound>> Borrar(int id, IRepositorioPeliculas repositorio,
            IAlmacenarArchivos almacenarArchivos, IOutputCacheStore outputCacheStore)
        {
            var peliculaAntigua = await repositorio.ObtenerXid(id);
            if (peliculaAntigua is null)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Borrar(id);
            await almacenarArchivos.Borrar(peliculaAntigua.Poster, carpeta);
            await outputCacheStore.EvictByTagAsync("peliculas-get", default);

            return TypedResults.NoContent();


        }

        static async Task<Results<NoContent, NotFound, BadRequest<String>>> AsignarGeneros(int id, List<int> generosId, IRepositorioPeliculas repositorioPeliculas,
            IRepositorioGeneros repositorioGeneros)
        {

            if (!await repositorioPeliculas.Existe(id))
            {
                return TypedResults.NotFound();
            }
            var generosExistentes = new List<int>();

            if (generosId.Count != 0)
            {
                generosExistentes = await repositorioGeneros.Existen(generosId);
            }
            if (generosExistentes.Count != generosId.Count)
            {
                var generosNoExistentes = generosId.Except(generosExistentes);


                return TypedResults.BadRequest($"Los generos de id {string.Join(",", generosNoExistentes)} no existen");

            }

            await repositorioPeliculas.AsignarGeneros(id, generosId);

            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent, BadRequest<String>>> AsignarActores(int id, List<AsignarActorPeliculaDTO> asignarActoresDTO,
            IRepositorioActores repositorioActores, IRepositorioPeliculas repositorioPeliculas, IMapper mapper)
        {
            // Comprueba si la película con el ID proporcionado existe
            if (!await repositorioPeliculas.Existe(id))
            {
                // Si no existe, devuelve un resultado NotFound
                return TypedResults.NotFound();
            }
            var actoresExistentes = new List<int>();
            // Obtiene los IDs de los actores a asignar
            var actoresIds = asignarActoresDTO.Select(x => x.ActorId).ToList();

            // Si hay actores a asignar
            if (asignarActoresDTO.Count != 0)
            {
                // Comprueba si los actores existen
                actoresExistentes = await repositorioActores.Existen(actoresIds);
            }
            // Si el número de actores existentes no coincide con el número de actores a asignar
            if (actoresExistentes.Count != asignarActoresDTO.Count)
            {
                // Encuentra los actores que no existen
                var actoresNoExistentes = actoresIds.Except(actoresExistentes);

                // Devuelve un resultado BadRequest con los IDs de los actores que no existen
                return TypedResults.BadRequest($"Los actores de id {string.Join(",", actoresNoExistentes)} no existen");
            }

            // Mapea los actores a asignar a entidades ActorPelicula
            var actores = mapper.Map<List<ActorPelicula>>(asignarActoresDTO);

            // Asigna los actores a la película
            await repositorioPeliculas.AsignarActores(id, actores);
            // Devuelve un resultado NoContent indicando que la operación fue exitosa
            return TypedResults.NoContent();
        }

        static async Task<Ok<List<PeliculaDTO>>> FiltrarPeliculas(PeliculasFiltrarDTO filtrarDTO, IRepositorioPeliculas repositorio, 
            IMapper mapper)
        {
            var peliculas = await repositorio.FiltrarPeliculas(filtrarDTO);
            var peliculasDTO = mapper.Map<List<PeliculaDTO>>(peliculas);
            return TypedResults.Ok(peliculasDTO);
        }


    }
}
