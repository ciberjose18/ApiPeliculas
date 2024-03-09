using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Migrations;
using ApiPeliculas.DTOs;
using AutoMapper;
using System.Runtime.ConstrainedExecution;

namespace ApiJuegos.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearGeneroDTO, Genero>();
            CreateMap<ActualizarGeneroDTO, Genero>();
            CreateMap<Genero, GeneroDTO>();

            // En este caso, la propiedad Foto de la clase Actor se está configurando para ser ignorada durante el mapeo
            // Esto significa que no se copiará la propiedad Foto de CrearActorDTO a Actor durante la operación de mapeo
            //ForMember nos pide la propiedad que a configurar y la configuracion que le vamos a asignar
            CreateMap<CrearActorDTO, Actor>().ForMember(x => x.Foto, op => op.Ignore());
            CreateMap<Actor, ActorDTO>();

            // Map de las peliculas
            CreateMap<CrearPeliculaDTO, Pelicula>().ForMember(x => x.Poster, op => op.Ignore());

            // Crear un mapeo de la clase Pelicula a la clase PeliculaDTO
            // Para la propiedad Generos de PeliculaDTO, estamos diciendo que se debe llenar con los datos de GeneroPeliculas de la entidad Pelicula
            // Para cada GeneroPelicula en la lista de GeneroPeliculas de la entidad Pelicula, creamos un nuevo GeneroDTO
            CreateMap<Pelicula, PeliculaDTO>().ForMember(x => x.Generos, entidad => entidad.MapFrom(p => p.GeneroPeliculas.Select(gp => new GeneroDTO
            {
                // El Id y el Nombre del GeneroDTO se llenan con los datos del Genero asociado a cada GeneroPelicula
                Id = gp.Genero.Id, Nombre = gp.Genero.Nombre
            })))
                // Para la propiedad Actores de PeliculaDTO, estamos diciendo que se debe llenar con los datos de ActoresPeliculas de la entidad Pelicula
                // Para cada ActorPelicula en la lista de ActoresPeliculas de la entidad Pelicula, creamos un nuevo ActorPeliculaDTO
                .ForMember(x => x.Actores, entidad => entidad.MapFrom(p => p.ActoresPeliculas.Select(ap => new ActorPeliculaDTO
            {
               // El ActorId, Nombre y Personaje del ActorPeliculaDTO se llenan con los datos del Actor y el Personaje asociados a cada ActorPelicula
                ActorId = ap.ActorId, 
                Nombre = ap.Actor.Nombre,
                Personaje = ap.Personaje,
            })));

            // Map de las Comentarios
            CreateMap<CrearComentarioDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();

            // Map para asignar actores a peliculas
            CreateMap<AsignarActorPeliculaDTO, ActorPelicula>();
        }
    }
}
