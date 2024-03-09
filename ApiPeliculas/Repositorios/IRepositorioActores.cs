using ApiJuegos.DTOs;
using ApiJuegos.Entidades;

namespace ApiJuegos.Repositorios
{
    public interface IRepositorioActores
    {
        Task Actualizar(Actor actor);
        Task Borrar(int id);
        Task<int> Crear(Actor actor);
        Task<bool> Existe(int id);
        Task<List<int>> Existen(List<int> ids);
        Task<List<Actor>> ObtenerTodos(PaginacionDTO paginacionDTO);
        Task<Actor?> ObtenerXid(int id);
        Task<List<Actor>> ObtenerXnombre(string nombre);
    }
}