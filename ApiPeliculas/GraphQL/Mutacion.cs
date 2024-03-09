using ApiJuegos.DTOs;
using ApiJuegos.Entidades;
using ApiJuegos.Repositorios;
using ApiPeliculas.DTOs;
using AutoMapper;
using HotChocolate.Authorization;
using System;
using System.Threading.Tasks;

namespace ApiPeliculas.GraphQL
{

    // En esta clase vamos a crear los metodos a forma de ejmplo para crear datos, por ejemplo InsertarGenero.
    //Esto ya lo estamos realizando con Rest API, pero ahora lo haremos con GraphQL
    public class Mutacion
    {
        [Serial]
        [Authorize(Policy = "admin")]
        public async Task<GeneroDTO> CrearGeneroGrql([Service] IRepositorioGeneros repositorioGeneros,
            [Service] IMapper mapper, CrearGeneroDTO crearGeneroDTO)
        {
            var genero = mapper.Map<Genero>(crearGeneroDTO);
            await repositorioGeneros.Crear(genero);
            var generoDTO = mapper.Map<GeneroDTO>(genero);
            return generoDTO;

        }

        [Serial]
        [Authorize(Policy = "admin")]
        public async Task<GeneroDTO?> ActualizarGeneroGrql([Service] IRepositorioGeneros repositorioGeneros,
            [Service] IMapper mapper, ActualizarGeneroDTO actualizarGeneroDTO)
        {
            var generoExiste = await repositorioGeneros.Existe(actualizarGeneroDTO.Id);

            if (!generoExiste)
            {
                return null;
            }
            var genero = mapper.Map<Genero>(actualizarGeneroDTO);
            await repositorioGeneros.Actualizar(genero);
            var generoDTO = mapper.Map<GeneroDTO>(genero);
            return generoDTO;

        }

        [Serial]
        [Authorize(Policy = "admin")]
        public async Task<bool> BorrarGeneroGrql([Service] IRepositorioGeneros repositorioGeneros, int id)
        {
            var generoExiste = await repositorioGeneros.Existe(id);

            if (!generoExiste)
            {
                return false;
            }

            await repositorioGeneros.Eliminar(id);
            return true;

        }
    }
}
