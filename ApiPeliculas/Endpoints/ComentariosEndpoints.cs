using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Filtros;
using ApiJuegos.Repositorios;
using ApiJuegos.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using System;
using System.Threading.Tasks;

namespace ApiJuegos.Endpoints
{
    public static class ComentariosEndpoints
    {
        public static RouteGroupBuilder MapComentarios(this RouteGroupBuilder group)
        {
            group.MapPost("/", Crear).AddEndpointFilter<FiltroValidaciones<CrearComentarioDTO>>().RequireAuthorization();
            group.MapGet("/", ObtenerTodos).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("comentarios-get")
            .SetVaryByRouteValue(new string[] { "peliculaId" }));
            group.MapGet("/{id:int}", ObtenerXid);
            group.MapPut("/{id:int}", Actualizar).RequireAuthorization().AddEndpointFilter<FiltroValidaciones<CrearComentarioDTO>>();
            group.MapDelete("/{id:int}", Borrar).RequireAuthorization();

            return group;
        }

        static async Task<Results<Created<ComentarioDTO>, NotFound, BadRequest<string>>> Crear(int peliculaId, CrearComentarioDTO crearComentarioDTO,
            IRepositorioComentarios repositorioComentarios, IRepositorioPeliculas repositorioPeliculas, IMapper mapper, 
            IOutputCacheStore outputCache, IServicioUsuarios servicioUsuarios)
        {
            if (!await repositorioPeliculas.Existe(peliculaId))
            {
                return TypedResults.NotFound();
            }
            //usar mapper, es como que el usuario llena los detalles del comentario en el DTO,
            //y mapper es como un traductor que convierte ese formulario en un Comentario que nuestra aplicación puede entender.
            var comentario = mapper.Map<Comentario>(crearComentarioDTO);
            // Asignamos el ID de la película al comentario.
            comentario.PeliculaId = peliculaId;
            // Intentamos obtener el usuario que está creando el comentario
            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {
                return TypedResults.BadRequest("Usuario no encontrado");
            }
            // Asignamos el ID del usuario al comentario.
            comentario.UsuarioId = usuario.Id;
            // Creamos el comentario en nuestra base de datos y obtenemos su ID.
            var id = await repositorioComentarios.Crear(comentario);
            // Borramos cualquier caché que tenga que ver con los comentarios.
            await outputCache.EvictByTagAsync("comentarios-get", default);
            
            // Convertimos nuestro Comentario de nuevo en un ComentarioDTO. Para que el usuario pueda verlo.
            var comentarioDTO = mapper.Map<ComentarioDTO>(comentario);

            // Devolvemos el ComentarioDTO en la respuesta, junto con el ID del nuevo comentario.
            // Esto es para que el usuario pueda ver el comentario que acaba de crear.
            return TypedResults.Created($"/comentarios/{id}", comentarioDTO);

        }

        static async Task<Results<Ok<List<ComentarioDTO>>, NotFound>> ObtenerTodos(int peliculaId, IRepositorioComentarios repositorioComentarios,
            IRepositorioPeliculas repositorioPeliculas, IMapper mapper)
        {
            if (!await repositorioPeliculas.Existe(peliculaId))
            {
                return TypedResults.NotFound();
            }
            var comentarios = await repositorioComentarios.ObtenerTodos(peliculaId);
            var comentariosDTO = mapper.Map<List<ComentarioDTO>>(comentarios);
            return TypedResults.Ok(comentariosDTO);

        }

        static async Task<Results<Ok<ComentarioDTO>, NotFound>> ObtenerXid(int peliculaId, int id, IRepositorioComentarios repositorio, 
            IMapper mapper)
        {
            var comentarios = await repositorio.ObtenerPorId(id);

            if (comentarios is null)
            {
                return TypedResults.NotFound();
            }
            var comentariosDTO = mapper.Map<ComentarioDTO>(comentarios);

            return TypedResults.Ok(comentariosDTO);
        }

        static async Task<Results<NoContent, NotFound, ForbidHttpResult>> Actualizar(int peliculaID, int id, CrearComentarioDTO crearComentarioDTO, IRepositorioPeliculas repositorioPeliculas, 
            IRepositorioComentarios repositorioComentarios, IOutputCacheStore outputCacheStore, IMapper mapper, IServicioUsuarios servicioUsuarios)
        {
            // Primero, verifica si la película para la que se está actualizando el comentario existe.
            if (!await repositorioPeliculas.Existe(peliculaID))
            {
                return TypedResults.NotFound();
            }
            // Luego, busca el comentario que se va a actualizar en la base de datos.
            var comentarioDB = await repositorioComentarios.ObtenerPorId(id);

            if (comentarioDB is null)
            {
                return TypedResults.NotFound();
            }
            // Luego, intenta obtener el usuario que está intentando hacer la actualización.
            var usuario = await servicioUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.NotFound();
            }

            // Verifica si el usuario que intenta hacer la actualización es el mismo que creó el comentario.
            if (comentarioDB.UsuarioId != usuario.Id)
            {
                // Si no es el mismo usuario, devuelve un error "Forbid".
                // se utiliza cuando un usuario intenta actualizar un comentario que no fue creado por él.
                //devuelve un estado HTTP 403, indicando que la acción está prohibida.
                return TypedResults.Forbid();
            }
            // Si todo está bien, actualiza el cuerpo del comentario con los nuevos datos.
            comentarioDB.Cuerpo = crearComentarioDTO.Cuerpo;
            // Luego, guarda la actualización en la base de datos.
            await repositorioComentarios.Actualizar(comentarioDB);
            // Borra cualquier caché relacionada con los comentarios para asegurarse de que los datos estén actualizados.
            await outputCacheStore.EvictByTagAsync("comentarios-get", default);
            // Finalmente, devuelve un resultado "NoContent" para indicar que la actualización fue exitosa.
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound, ForbidHttpResult>> Borrar(int peliculaID, int id, IRepositorioComentarios repositorioComentarios,
            IOutputCacheStore outputCacheStore, IServicioUsuarios servicioUsuarios)
        {

            if (!await repositorioComentarios.Existe(id))
            {
                return TypedResults.NotFound();
            }
            // Luego, busca el comentario que se va a eliminar en la base de datos.
            var comentarioDB = await repositorioComentarios.ObtenerPorId(id);

            if (comentarioDB is null)
            {
                return TypedResults.NotFound();
            }
            // Luego, intenta obtener el usuario que está intentando hacer la actualización.
            var usuario = await servicioUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.NotFound();
            }

            // Verifica si el usuario que intenta eliminar el comentario es el que creó el comentario.
            if (comentarioDB.UsuarioId != usuario.Id)
            {
                // Si no es el mismo usuario, devuelve un error "Forbid".
                // se utiliza cuando un usuario intenta actualizar un comentario que no fue creado por él.
                //devuelve un estado HTTP 403, indicando que la acción está prohibida.
                return TypedResults.Forbid();
            }
            await repositorioComentarios.Borrar(id);
            await outputCacheStore.EvictByTagAsync("comentarios-get", default);
            return TypedResults.NoContent();

        }

        
    }
}
