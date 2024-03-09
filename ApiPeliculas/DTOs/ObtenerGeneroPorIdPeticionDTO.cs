using ApiJuegos.Repositorios;
using AutoMapper;

namespace ApiJuegos.DTOs
{
    public class ObtenerGeneroPorIdPeticionDTO
    {
        public int Id { get; set; }
        public IRepositorioGeneros Repositorio { get; set; }
        public IMapper Mapper { get; set; }
    }
}
