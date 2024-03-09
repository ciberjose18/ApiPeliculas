using ApiJuegos.Repositorios;
using AutoMapper;
using Microsoft.AspNetCore.OutputCaching;

namespace ApiJuegos.DTOs
{
    public class CrearGeneroPeticionDTO
    {
        public IRepositorioGeneros Repositorio { get; set; }
        public IOutputCacheStore OutputCacheStore { get; set; }
        public IMapper Mapper { get; set; }
    }
}
