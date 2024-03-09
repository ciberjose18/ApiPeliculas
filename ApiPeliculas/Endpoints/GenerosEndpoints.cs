using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Filtros;
using ApiJuegos.Repositorios;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApiJuegos.Endpoints
{
    public static class GenerosEndpoints
    {
        public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerGeneros)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));

            group.MapGet("/{id:int}", ObtenerGeneroXid);

            group.MapPost("/", CrearGenero).AddEndpointFilter<FiltroValidaciones<CrearGeneroDTO>>().RequireAuthorization("admin");

            group.MapPut("/{id:int}", ActualizarGenero).AddEndpointFilter<FiltroValidaciones<CrearGeneroDTO>>().RequireAuthorization("admin")
                .WithOpenApi(op =>
                {
                    op.Summary = "Actualiza un genero";
                    op.Description = "Actualiza un genero";
                    op.Parameters[0].Description = " El id del genero a actualizar";
                    op.RequestBody.Description = "El genero a actualizar";

                    return op;
                });

            group.MapDelete("/{id:int}", BorrarGenero).RequireAuthorization("admin");
            return group;
        }



        //Ahora vamos a generar lo metodos

        static async Task<Ok<List<GeneroDTO>>> ObtenerGeneros(IRepositorioGeneros repositorio, IMapper mapper,ILoggerFactory loggerFactory)
        {
            // Obtenemos el tipo de la clase GenerosEndpoints
            var tipo = typeof(GenerosEndpoints);
            // Creamos un nuevo logger asociado con el nombre completo del tipo de la clase GenerosEndpoints
            var logger = loggerFactory.CreateLogger(tipo.FullName!);
            logger.LogTrace("Mensaje Trace");
            logger.LogDebug("Mensaje Debug");
            logger.LogInformation("Mensaje Informatio");
            logger.LogWarning("Mensaje Warning");
            logger.LogError("Mensaje Error");
            logger.LogCritical("Mensaje Critical");
            var generos = await repositorio.ObtenerTodos();
            // Crear una nueva lista llamada GeneroDTO
            var generoDTO = mapper.Map<List<GeneroDTO>>(generos);

            return TypedResults.Ok(generoDTO);

        }


        /* Todo esto se esta haciendo con una expresion lambda
         endPointGeneros.MapGet("/", async (IRepositorioGeneros repositorio) =>
        {

            return await repositorio.ObtenerTodos();
        }).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));
        Pero tambien se puede hacer con metedos estaticos, como se hara a continuacion */

        //AsParameters, es un contenedor de inyeccion de dependencias 
        static async Task<Results<Ok<GeneroDTO>, NotFound>> ObtenerGeneroXid([AsParameters] ObtenerGeneroPorIdPeticionDTO obtenerGenero)
        {
            var genero = await obtenerGenero.Repositorio.ObtenerPorId(obtenerGenero.Id);

            if (genero is null)
            {
                return TypedResults.NotFound();
            }
            // DTO objeto de transferencia de datos
            var generoDTO = obtenerGenero.Mapper.Map<GeneroDTO>(genero);


            return TypedResults.Ok(generoDTO);

        }


        static async Task<Results<Created<GeneroDTO>, ValidationProblem>> CrearGenero(CrearGeneroDTO crearGeneroDTO,
            [AsParameters] CrearGeneroPeticionDTO generoPeticionDTO)
        {
            var genero = generoPeticionDTO.Mapper.Map<Genero>(crearGeneroDTO);

            var Id = await generoPeticionDTO.Repositorio.Crear(genero);
            await generoPeticionDTO.OutputCacheStore.EvictByTagAsync("generos-get", default);  // todo esto del outputCacheStore y del Tag("generos-get") es para actualizar y limpiar el cache.

            /* Empieza de adentro hacia afuera
            var GeneroDTO = new GeneroDTO
            {
                Id = Id,
                Nombre = genero.Nombre
            };*/

            var generoDTO = generoPeticionDTO.Mapper.Map<GeneroDTO>(genero);

            return TypedResults.Created($"/generos/{Id}", generoDTO);
        }

        static async Task<Results<NoContent, NotFound, ValidationProblem>> ActualizarGenero(int id, CrearGeneroDTO crearGeneroDTO, IRepositorioGeneros repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            

            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var genero = mapper.Map<Genero>(crearGeneroDTO);
            genero.Id = id;
            await repositorio.Actualizar(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default);

            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent>> BorrarGenero(int id, IRepositorioGeneros repositorio, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Eliminar(id);
            await outputCacheStore.EvictByTagAsync("generos-get", default);
            return TypedResults.NoContent();

        }

    }
}
