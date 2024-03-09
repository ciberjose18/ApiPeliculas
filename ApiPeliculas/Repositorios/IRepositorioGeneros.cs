using ApiJuegos.Entidades;

namespace ApiJuegos.Repositorios
{
    public interface IRepositorioGeneros
    {
        Task<List<Genero?>> ObtenerTodos();
        Task<Genero> ObtenerPorId(int id);
        Task<int> Crear(Genero genero);
        Task<bool> Existe(int id);
        Task Actualizar(Genero genero);
        Task Eliminar(int id);
        Task<List<int>> Existen(List<int> ids);
        Task<bool> YaExiste(int id, string nombre);
       /* Task<bool> YaExiste2(string nombre, CancellationToken cancellation);*/
    }
}
