using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Filtros;
using ApiJuegos.Repositorios;
using ApiJuegos.Servicios;
using ApiJuegos.Utilidades;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ApiJuegos.Endpoints
{
    public static class ActoresEndpoints
    {
        private static readonly string carpeta = "actores";

        // Método para mapear las rutas relacionadas con los actores en el enrutador
        public static RouteGroupBuilder MapActores(this RouteGroupBuilder group)
        {
            // Mapear la ruta para obtener todos los actores mediante una solicitud GET
            // Configurar la caché para expirar en 60 segundos y etiquetarla como "actores-get"
            group.MapGet("/", ObtenerActores).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("actores-get"))
                .AgregarParametrosPaginacionAOpenAPI();

            group.MapGet("obtenerXnombre/{nombre}", ObtenerXnombre);

            group.MapGet("/{id:int}", ObternerXid);
            group.MapPost("/", Crear).DisableAntiforgery().AddEndpointFilter<FiltroValidaciones<CrearActorDTO>>().RequireAuthorization("admin")
                .WithOpenApi();
            // Mapear la ruta para actualizar un actor mediante una solicitud PUT con un parámetro de ruta de tipo entero (ID)
            // Deshabilitar la protección contra falsificación de solicitudes (Antiforgery)
            group.MapPut("/{id:int}", Actualizar).DisableAntiforgery().AddEndpointFilter<FiltroValidaciones<CrearActorDTO>>().
                RequireAuthorization("admin").WithOpenApi();
            group.MapDelete("/{id:int}", Borrar).RequireAuthorization("admin");


            // Devolver el grupo de rutas actualizado
            return group;
        }

        //Vamos a recibir desde un DTO los valores de paginacion, para respetar el principio de responsabilidad única
        static async Task<Ok<List<ActorDTO>>> ObtenerActores(IRepositorioActores repositorio, IMapper mapper, PaginacionDTO paginacion)
        {
            //var paginacion = new PaginacionDTO { Pagina = pagina, RecordsXpagina = recordsXpagina };

            var actores = await repositorio.ObtenerTodos(paginacion);
            var actoresDTO = mapper.Map<List<ActorDTO>>(actores);

            return TypedResults.Ok(actoresDTO);
        }

        static async Task<Ok<List<ActorDTO>>> ObtenerXnombre(string nombre, IRepositorioActores repositorio, IMapper mapper)
        {
            var actores = await repositorio.ObtenerXnombre(nombre);
            var actoresDTO = mapper.Map<List<ActorDTO>>(actores);

            return TypedResults.Ok(actoresDTO);
        }


        static async Task<Results<Ok<ActorDTO>, NotFound>> ObternerXid(int id, IRepositorioActores repositorio, IMapper mapper)
        {
            var actores = await repositorio.ObtenerXid(id);

            if (actores is null)
            {
                return TypedResults.NotFound();
            }

            var actorDTO = mapper.Map<ActorDTO>(actores);

            return TypedResults.Ok(actorDTO);
        }




        // Recibe la información del nuevo actor desde el formulario
        // El lugar mágico donde se guarda la información de los actores
        // El almacén donde se guarda información para que no se tenga que buscar siempre de nuevo el cache
        // El traductor que convierte la información de un formato a otro
        //FromForm se usa para la foto
        static async Task<Results<Created<ActorDTO>,ValidationProblem>> Crear([FromForm] CrearActorDTO crearActorDTO, IRepositorioActores repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper, IAlmacenarArchivos almacenarArchivos)
        {
            // Convierte la información del formulario en un objeto "actor"
            var actor = mapper.Map<Actor>(crearActorDTO);

            if (crearActorDTO.Foto is not null)
            {
                var url = await almacenarArchivos.Almacenar(carpeta, crearActorDTO.Foto);
                actor.Foto = url;
            }

            // Guarda el actor en el repositoria y devuelve el ID
            var id = await repositorio.Crear(actor);
            // Elimina la información de los actores del cache
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            // Convierte el objeto "actor" en un "actorDto" para el usuario
            var actorDto = mapper.Map<ActorDTO>(actor);
            // Devuelve el resultado con la información del nuevo actor
            return TypedResults.Created($"/actores/{id}", actorDto);

        }

        // Método para actualizar un actor
        static async Task<Results<NoContent, NotFound>> Actualizar(int id, [FromForm] CrearActorDTO crearActorDTO,
            IRepositorioActores repositorio, IAlmacenarArchivos almacenarArchivos, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            // Obtener el actor de la base de datos por su ID
            var actorAntiguo = await repositorio.ObtenerXid(id);

            // Verificar si el actor no existe en la base de datos
            if (actorAntiguo is null)
            {
                // Devolver un resultado NotFound
                return TypedResults.NotFound();
            }

            // Mapear los datos del DTO a un objeto Actor
            var actorParaActualizar = mapper.Map<Actor>(crearActorDTO);
            // Asignar el ID al actorParaActualizar
            actorParaActualizar.Id = id;
            // Mantener la foto existente del actor para evitar cambios si no se proporciona una nueva

            actorParaActualizar.Foto = actorAntiguo.Foto;

            // Verificar si se proporcionó una nueva foto en el DTO
            if (crearActorDTO.Foto is not null)
            {
                // Almacenar la nueva foto y obtener la URL
                var url = await almacenarArchivos.Editar(actorParaActualizar.Foto, carpeta, crearActorDTO.Foto);
                // Actualizar la propiedad Foto del actor con la nueva URL
                actorParaActualizar.Foto = url;

            }
            // Actualizar el actor en la base de datos
            await repositorio.Actualizar(actorParaActualizar);
            // Invalidar la caché relacionada con los actores
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            // Devolver un resultado NoContent indicando que la actualización fue exitosa
            return TypedResults.NoContent();

        }

        static async Task<Results<NoContent, NotFound>> Borrar(int id, IRepositorioActores repositorio, IOutputCacheStore outputCacheStore,
            IAlmacenarArchivos almacenarArchivos)
        {
            var actorAntiguo = await repositorio.ObtenerXid(id);

            if (actorAntiguo is null)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Borrar(id);
            await almacenarArchivos.Borrar(actorAntiguo.Foto, carpeta);
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            return TypedResults.NoContent();


        }

    }
}
