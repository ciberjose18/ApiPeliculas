using ApiJuegos.Entidades;
using Error = ApiJuegos.Entidades.Error;

namespace ApiJuegos.Repositorios
{
    public interface IRepositorioErrores
    {
        Task Crear(Error error);
    }
}