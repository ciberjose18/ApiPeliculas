using ApiJuegos.DTOs;
using ApiJuegos.Entidades;

namespace ApiJuegos.Repositorios
{
    public interface IRepositorioPeliculas
    {
        Task Actualizar(Pelicula pelicula);
        Task AsignarActores(int id, List<ActorPelicula> actorPeliculas);
        Task AsignarGeneros(int id, List<int> generosId);
        Task Borrar(int id);
        Task<int> Crear(Pelicula pelicula);
        Task<bool> Existe(int id);
        Task<List<Pelicula>> ObtenerTodos(PaginacionDTO paginacionDTO);
        Task<Pelicula?> ObtenerXid(int id);
        Task<List<Pelicula>> FiltrarPeliculas(PeliculasFiltrarDTO filtrarDTO);

    }
}